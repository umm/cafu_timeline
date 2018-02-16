using System;
using System.Collections.Generic;
using System.Linq;
using CAFU.Core.Data.DataStore;
using CAFU.Timeline.Data.Entity;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;
// ReSharper disable ArrangeAccessorOwnerBody
// ReSharper disable UseNullPropagation
// ReSharper disable UseStringInterpolation

namespace CAFU.Timeline.Data.DataStore.Scene {

    public abstract class TimelineDataStore<TEnum, TTimelineEntity> : ObservableLifecycleMonoBehaviour, ITimelineDataStore<TEnum, TTimelineEntity>
        where TEnum : struct
        where TTimelineEntity : ITimelineEntity {

        public class Factory : SceneDataStoreFactory<TimelineDataStore<TEnum, TTimelineEntity>> {

        }

        [SerializeField]
        private List<TTimelineEntity> definedTimelineEntityList;

        private IEnumerable<ITimelineEntity> DefinedTimelineEntityList {
            get {
                return this.definedTimelineEntityList.Cast<ITimelineEntity>();
            }
        }

        private List<ITimelineEntity> TimelineEntityList { get; set; }

        public PlayableDirector GetPlayableDirector(TEnum timelineName) {
            // 定義済リストをコピーする
            if (this.TimelineEntityList == default(List<ITimelineEntity>)) {
                this.TimelineEntityList = this.DefinedTimelineEntityList.ToList();
            }
            if (!this.TimelineEntityList.Any(x => ((ITimelineEntity<TEnum>)x).Name.Equals(timelineName))) {
                this.AddTimelineEntityFromTransform(timelineName);
            }
            ITimelineEntity timelineEntity = this.TimelineEntityList.Find(x => ((ITimelineEntity<TEnum>)x).Name.Equals(timelineName));
            if (timelineEntity == default(ITimelineEntity)) {
                return null;
            }
            return ((ITimelineEntity<TEnum>)timelineEntity).PlayableDirector;
        }

        private void AddTimelineEntityFromTransform(TEnum timelineName) {
            // enum のアンダースコアをスラッシュに置換して、Hierarchy を探す
            Transform playableDirectorTransform = this.transform.Find(timelineName.ToString().Replace("_", "/"));
            // 見付からなかった場合に、配下の全 Transform の名前を enum の完全一致で探す
            if (playableDirectorTransform == default(Transform)) {
                playableDirectorTransform = this.transform.GetComponentsInChildren<Transform>(true).ToList().Find(x => x.name == timelineName.ToString());
            }
            // それでも見付からなかった場合は Exception を throw する
            if (playableDirectorTransform == default(Transform)) {
                throw new ArgumentException(string.Format("TimelineName '{0}' does not found.", timelineName));
            }
            this.TimelineEntityList.Add(
                new TimelineEntity<TEnum>() {
                     Name = timelineName,
                     PlayableDirector = playableDirectorTransform.GetComponent<PlayableDirector>(),
                }
            );
        }

    }

}