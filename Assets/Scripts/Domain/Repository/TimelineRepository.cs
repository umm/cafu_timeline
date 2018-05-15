using CAFU.Core.Domain.Repository;
using CAFU.Timeline.Data.DataStore;
using JetBrains.Annotations;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.Repository
{
    public interface ITimelineRepository<in TEnum> : IRepository where TEnum : struct
    {
        PlayableDirector GetPlayableDirector(TEnum name);
    }

    [PublicAPI]
    public class TimelineRepository<TEnum> : ITimelineRepository<TEnum>
        where TEnum : struct
    {
        public class Factory : DefaultRepositoryFactory<TimelineRepository<TEnum>>
        {
            protected override void Initialize(TimelineRepository<TEnum> instance)
            {
                base.Initialize(instance);
                instance.TimelineDataStore = new TimelineDataStore<TEnum>.Factory().Create();
            }
        }

        private ITimelineDataStore<TEnum> TimelineDataStore { get; set; }

        public PlayableDirector GetPlayableDirector(TEnum name)
        {
            return TimelineDataStore.GetPlayableDirector(name);
        }
    }
}