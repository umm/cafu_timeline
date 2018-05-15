using CAFU.Core.Domain.Repository;
using CAFU.Timeline.Data.DataStore;
using CAFU.Timeline.Data.Entity;
using JetBrains.Annotations;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.Repository
{
    public interface ITimelineRepository<in TEnum> : IRepository where TEnum : struct
    {
        PlayableDirector GetPlayableDirector(TEnum name);
    }

    [PublicAPI]
    public class TimelineRepository<TEnum, TTimelineEntity> : ITimelineRepository<TEnum>
        where TEnum : struct
        where TTimelineEntity : ITimelineEntity<TEnum>
    {
        public class Factory : DefaultRepositoryFactory<TimelineRepository<TEnum, TTimelineEntity>>
        {
            protected override void Initialize(TimelineRepository<TEnum, TTimelineEntity> instance)
            {
                base.Initialize(instance);
                instance.TimelineDataStore = new TimelineDataStore<TEnum, TTimelineEntity>.Factory().Create();
            }
        }

        private ITimelineDataStore<TEnum> TimelineDataStore { get; set; }

        public PlayableDirector GetPlayableDirector(TEnum name)
        {
            return TimelineDataStore.GetPlayableDirector(name);
        }
    }
}