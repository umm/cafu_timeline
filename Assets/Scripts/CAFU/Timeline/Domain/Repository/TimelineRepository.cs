using CAFU.Core.Domain.Repository;
using CAFU.Timeline.Data.DataStore.Scene;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.Repository {

    public interface ITimelineRepository : IRepository {

    }

    public interface ITimelineRepository<in TEnum> : ITimelineRepository
        where TEnum : struct {

        PlayableDirector GetPlayableDirector(TEnum name);

    }

    public class TimelineRepository<TEnum> : ITimelineRepository<TEnum>
        where TEnum : struct {

        public class Factory : DefaultRepositoryFactory<Factory, TimelineRepository<TEnum>> {

            protected override void Initialize(TimelineRepository<TEnum> instance) {
                base.Initialize(instance);
                instance.TimelineDataStore = new TimelineDataStore<TEnum>.Factory().Create();
            }

        }

        private TimelineDataStore<TEnum> TimelineDataStore { get; set; }

        public PlayableDirector GetPlayableDirector(TEnum name) {
            return this.TimelineDataStore.GetPlayableDirector(name);
        }

    }

}