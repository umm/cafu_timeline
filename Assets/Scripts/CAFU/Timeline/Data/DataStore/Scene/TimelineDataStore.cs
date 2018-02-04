using System;
using System.Collections.Generic;
using System.Linq;
using CAFU.Core.Data.DataStore;
using CAFU.Timeline.Data.Entity;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;

namespace CAFU.Timeline.Data.DataStore.Scene {

    public abstract class TimelineDataStore<TTimelineEntity> : ObservableLifecycleMonoBehaviour, ITimelineDataStore
        where TTimelineEntity : class, ITimelineEntity, new() {

        [SerializeField]
        private List<TTimelineEntity> timelineEntityList;

        private List<TTimelineEntity> TimelineEntityList => this.timelineEntityList;

        public PlayableDirector GetPlayableDirector<TEnum>(TEnum timelineName) where TEnum : struct {
            if (!this.TimelineEntityList.OfType<TimelineEntity<TEnum>>().Any(x => x.Name.Equals(timelineName))) {
                this.AddTimelineEntityFromTransform(timelineName);
            }
            return this.TimelineEntityList.OfType<TimelineEntity<TEnum>>().ToList().Find(x => x.Name.Equals(timelineName)).PlayableDirector;
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
            TimelineEntity<TEnum> timelineEntity = new TTimelineEntity() as TimelineEntity<TEnum>;
            if (timelineEntity == null) {
                return;
            }
            timelineEntity.Name = timelineName;
            timelineEntity.PlayableDirector = playableDirectorTransform.GetComponent<PlayableDirector>();
            this.TimelineEntityList.Add(timelineEntity as TTimelineEntity);
        }

        public class Factory : IDataStoreFactory<TimelineDataStore<TTimelineEntity>> {

            public TimelineDataStore<TTimelineEntity> Create() {
                return FindObjectOfType<TimelineDataStore<TTimelineEntity>>();
            }

        }

    }

}