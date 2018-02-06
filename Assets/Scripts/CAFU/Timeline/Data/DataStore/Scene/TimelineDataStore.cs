using System;
using System.Collections.Generic;
using System.Linq;
using CAFU.Core.Data.DataStore;
using CAFU.Timeline.Data.Entity;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;

namespace CAFU.Timeline.Data.DataStore.Scene {

    public interface ITimelineDataStoreController<TEnum>
        where TEnum : struct {

        [ObservableAwakeMonoBehaviour]
        // ReSharper disable once UnusedMember.Global
        TimelineDataStore<TEnum> TimelineDataStore { get; }

    }

    public abstract class TimelineDataStore<TEnum> : ObservableLifecycleMonoBehaviour, ITimelineDataStore<TEnum>
        where TEnum : struct {

        public class Factory : SceneDataStoreFactory<Factory, TimelineDataStore<TEnum>> {

        }

        protected abstract IEnumerable<ITimelineEntity<TEnum>> DefinedTimelineEntityList { get; }

        private List<ITimelineEntity<TEnum>> TimelineEntityList { get; set; }

        public PlayableDirector GetPlayableDirector(TEnum timelineName) {
            // 定義済リストをコピーする
            if (this.TimelineEntityList == default(List<ITimelineEntity<TEnum>>)) {
                this.TimelineEntityList = this.DefinedTimelineEntityList.ToList();
            }
            if (!this.TimelineEntityList.Any(x => x.Name.Equals(timelineName))) {
                this.AddTimelineEntityFromTransform(timelineName);
            }
            return this.TimelineEntityList.Find(x => x.Name.Equals(timelineName)).PlayableDirector;
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
                new TimelineEntity<TEnum>() {
                     Name = timelineName,
                     PlayableDirector = playableDirectorTransform.GetComponent<PlayableDirector>(),
                }
            );
        }

    }

}