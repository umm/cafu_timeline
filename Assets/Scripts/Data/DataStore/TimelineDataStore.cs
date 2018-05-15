using System;
using System.Collections.Generic;
using System.Linq;
using CAFU.Core.Data.DataStore;
using CAFU.Timeline.Data.Entity;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;

namespace CAFU.Timeline.Data.DataStore
{
    public interface ITimelineDataStore<in TEnum> : IDataStore where TEnum : struct
    {
        PlayableDirector GetPlayableDirector(TEnum timelineName);
    }

    public abstract class TimelineDataStore<TEnum, TTimelineEntity> : ObservableLifecycleMonoBehaviour, ITimelineDataStore<TEnum>
        where TEnum : struct
        where TTimelineEntity : ITimelineEntity
    {
        public class Factory : SceneDataStoreFactory<TimelineDataStore<TEnum, TTimelineEntity>>
        {
        }

        [SerializeField] private List<TTimelineEntity> definedTimelineEntityList;

        private IEnumerable<ITimelineEntity> DefinedTimelineEntityList => definedTimelineEntityList.Cast<ITimelineEntity>();

        private List<ITimelineEntity> TimelineEntityList { get; set; }

        public PlayableDirector GetPlayableDirector(TEnum timelineName)
        {
            // 定義済リストをコピーする
            if (TimelineEntityList == default(List<ITimelineEntity>))
            {
                TimelineEntityList = DefinedTimelineEntityList.ToList();
            }

            if (!TimelineEntityList.Any(x => ((ITimelineEntity<TEnum>) x).Name.Equals(timelineName)))
            {
                AddTimelineEntityFromTransform(timelineName);
            }

            var timelineEntity = TimelineEntityList.Find(x => ((ITimelineEntity<TEnum>) x).Name.Equals(timelineName));
            return ((ITimelineEntity<TEnum>) timelineEntity)?.PlayableDirector;
        }

        private void AddTimelineEntityFromTransform(TEnum timelineName)
        {
            // enum のアンダースコアをスラッシュに置換して、Hierarchy を探す
            var playableDirectorTransform = transform.Find(timelineName.ToString().Replace("_", "/"));
            // 見付からなかった場合に、配下の全 Transform の名前を enum の完全一致で探す
            if (playableDirectorTransform == default(Transform))
            {
                playableDirectorTransform = transform.GetComponentsInChildren<Transform>(true).ToList().Find(x => x.name == timelineName.ToString());
            }

            // それでも見付からなかった場合は Exception を throw する
            if (playableDirectorTransform == default(Transform))
            {
                throw new ArgumentException($"TimelineName '{timelineName}' does not found.");
            }

            TimelineEntityList.Add(
                new TimelineEntity<TEnum>()
                {
                    Name = timelineName,
                    PlayableDirector = playableDirectorTransform.GetComponent<PlayableDirector>(),
                }
            );
        }
    }
}