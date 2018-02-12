using System;
using System.Collections.Generic;
using System.Linq;
using CAFU.Core.Data.DataStore;
using CAFU.Timeline.Data.Entity;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;

// ReSharper disable ArrangeAccessorOwnerBody

namespace CAFU.Timeline.Data.DataStore {

    public interface ITimelineDataStore<in TEnum, out TTimelineEntity> : IDataStore where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum> {

        TTimelineEntity GetTimelineEntity(TEnum timelineName);

        PlayableDirector GetPlayableDirector(TEnum timelineName);

    }

    public abstract class TimelineDataStore<TEnum, TTimelineEntity> : ObservableLifecycleMonoBehaviour, ITimelineDataStore<TEnum, TTimelineEntity>
        where TEnum : struct
        where TTimelineEntity : ITimelineEntity<TEnum>, new() {

        public class Factory : SceneDataStoreFactory<TimelineDataStore<TEnum, TTimelineEntity>> {

        }

        [SerializeField]
        private List<TTimelineEntity> definedTimelineEntityList;

        private IEnumerable<ITimelineEntity<TEnum>> DefinedTimelineEntityList {
            get {
                return this.definedTimelineEntityList.Cast<ITimelineEntity<TEnum>>();
            }
        }

        private List<ITimelineEntity<TEnum>> TimelineEntityList { get; set; }

        protected override void OnAwake() {
            base.OnAwake();
            ProjectContext.Instance.Container.Bind<ITimelineDataStore<TEnum, TTimelineEntity>>().FromInstance(this);
        }

        public TTimelineEntity GetTimelineEntity(TEnum timelineName) {
            this.PrepareTimelineEntityList(timelineName);
            return (TTimelineEntity)this.TimelineEntityList.Find(x => x.Name.Equals(timelineName));
        }

        public PlayableDirector GetPlayableDirector(TEnum timelineName) {
            return this.GetTimelineEntity(timelineName)?.PlayableDirector;
        }

        private void PrepareTimelineEntityList(TEnum timelineName) {
            // 定義済リストをコピーする
            if (this.TimelineEntityList == default(List<ITimelineEntity<TEnum>>)) {
                this.TimelineEntityList = this.DefinedTimelineEntityList.ToList();
            }
            if (!this.TimelineEntityList.Any(x => x.Name.Equals(timelineName))) {
                this.AddTimelineEntityFromTransform(timelineName);
            }
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
                throw new ArgumentException($"TimelineName '{timelineName}' does not found.");
            }
            this.TimelineEntityList.Add(
                new TTimelineEntity() {
                     Name = timelineName,
                     PlayableDirector = playableDirectorTransform.GetComponent<PlayableDirector>(),
                }
            );
        }

    }

}
