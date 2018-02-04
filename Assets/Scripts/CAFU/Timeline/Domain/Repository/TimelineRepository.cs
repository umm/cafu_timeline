using CAFU.Core.Domain.Repository;
using CAFU.Timeline.Data.DataStore;
using CAFU.Timeline.Data.DataStore.Scene;
using CAFU.Timeline.Data.Entity;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.Repository {

    public interface ITimelineRepository : IRepository {

        PlayableDirector GetPlayableDirector<TEnum>(TEnum name) where TEnum : struct;

    }

    public class TimelineRepository<TTimelineEntity> : ITimelineRepository
        where TTimelineEntity : class, ITimelineEntity, new() {

        private ITimelineDataStore TimelineDataStore { get; }

        public TimelineRepository() {
            // FIXME: Use Zenject
            this.TimelineDataStore = new TimelineDataStore<TTimelineEntity>.Factory().Create();
        }

        public PlayableDirector GetPlayableDirector<TEnum>(TEnum name) where TEnum : struct {
            return this.TimelineDataStore.GetPlayableDirector(name);
        }

    }

}