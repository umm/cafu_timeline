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

namespace CAFU.Timeline.Data.DataStore.Scene {

    public interface ITimelineDataStoreController {

        [ObservableAwakeMonoBehaviour]
        // ReSharper disable once UnusedMember.Global
        ITimelineDataStore TimelineDataStore { get; }

    }

    public abstract class TimelineDataStore<TTimelineEntity> : ObservableLifecycleMonoBehaviour, ITimelineDataStore
        where TTimelineEntity : ITimelineEntity {

        public class Factory : SceneDataStoreFactory<Factory, TimelineDataStore<TTimelineEntity>> {

        }

        [SerializeField]
        private List<TTimelineEntity> definedTimelineEntityList;

        private IEnumerable<ITimelineEntity> DefinedTimelineEntityList {
            get {
                return this.definedTimelineEntityList.Cast<ITimelineEntity>();
            }
        }

        private List<ITimelineEntity> TimelineEntityList { get; set; }

        public PlayableDirector GetPlayableDirector<TEnum>(TEnum timelineName) where TEnum : struct {
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

        private void AddTimelineEntityFromTransform<TEnum>(TEnum timelineName) where TEnum : struct {
            // enum のアンダースコアをスラッシュに置換して、Hierarchy を探す
            Transform playableDirectorTransform = this.transform.Find(timelineName.ToString().Replace("_", "/"));
            // 見付からなかった場合に、配下の全 Transform の名前を enum の完全一致で探す
            if (playableDirectorTransform == default(Transform)) {
                playableDirectorTransform = this.transform.GetComponentsInChildren<Transform>(true).ToList().Find(x => x.name == timelineName.ToString());
            }
            // それでも見付からなかった場合は Exception を throw する
            if (playableDirectorTransform == default(Transform)) {
                throw new ArgumentException($"TimelineName '{timelineName}' does not found.");
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