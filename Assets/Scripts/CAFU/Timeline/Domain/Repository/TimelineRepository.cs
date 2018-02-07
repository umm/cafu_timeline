using CAFU.Core.Domain.Repository;
using CAFU.Timeline.Data.DataStore.Scene;
using CAFU.Timeline.Data.Entity;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.Repository {

    public interface ITimelineRepository : IRepository {

        PlayableDirector GetPlayableDirector<TEnum>(TEnum name) where TEnum : struct;

    }

    public class TimelineRepository<TTimelineEntity> : ITimelineRepository
        where TTimelineEntity : ITimelineEntity {

        public class Factory : DefaultRepositoryFactory<Factory, TimelineRepository<TTimelineEntity>> {

            protected override void Initialize(TimelineRepository<TTimelineEntity> instance) {
                base.Initialize(instance);
                instance.TimelineDataStore = new TimelineDataStore<TTimelineEntity>.Factory().Create();
            }

        }

        private TimelineDataStore<TTimelineEntity> TimelineDataStore { get; set; }

        public PlayableDirector GetPlayableDirector<TEnum>(TEnum name) where TEnum : struct {
            return this.TimelineDataStore.GetPlayableDirector(name);
        }

    }

}