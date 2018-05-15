using CAFU.Core.Domain.Repository;
using CAFU.Timeline.Data.DataStore;
using CAFU.Timeline.Data.Entity;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.Repository {

    public interface ITimelineRepository<in TEnum, TTimelineEntity> : IRepository where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum> {

        ITimelineDataStore<TEnum, TTimelineEntity> TimelineDataStore { get; }

        PlayableDirector GetPlayableDirector(TEnum name);

    }

    public class TimelineRepository<TEnum, TTimelineEntity> : ITimelineRepository<TEnum, TTimelineEntity>
        where TEnum : struct
        where TTimelineEntity : ITimelineEntity<TEnum> {

        public class Factory : DefaultRepositoryFactory<TimelineRepository<TEnum, TTimelineEntity>> {

            protected override void Initialize(TimelineRepository<TEnum, TTimelineEntity> instance) {
                base.Initialize(instance);
                instance.TimelineDataStore = new TimelineDataStore<TEnum, TTimelineEntity>.Factory().Create();
            }

        }

        public ITimelineDataStore<TEnum, TTimelineEntity> TimelineDataStore { get; private set; }

        public PlayableDirector GetPlayableDirector(TEnum name) {
            return this.TimelineDataStore.GetPlayableDirector(name);
        }

    }

}