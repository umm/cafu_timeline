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

        public class Factory : IRepositoryFactory<TimelineRepository<TEnum>> {

            public TimelineRepository<TEnum> Create() {
                TimelineRepository<TEnum> timelineRepository = new TimelineRepository<TEnum> {
                    TimelineDataStore = new TimelineDataStore<TEnum>.Factory().Create()
                };
                return timelineRepository;
            }

        }

        private TimelineDataStore<TEnum> TimelineDataStore { get; set; }

        public PlayableDirector GetPlayableDirector(TEnum name) {
            return this.TimelineDataStore.GetPlayableDirector(name);
        }

    }

}