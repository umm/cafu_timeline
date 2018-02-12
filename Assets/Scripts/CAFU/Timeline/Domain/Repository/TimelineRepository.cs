using CAFU.Core.Domain.Repository;
using CAFU.Timeline.Data.DataStore;
using CAFU.Timeline.Data.Entity;
using UnityEngine.Playables;
using Zenject;

namespace CAFU.Timeline.Domain.Repository {

    public interface ITimelineRepository<in TEnum, out TTimelineEntity> : IRepository where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum> {

        ITimelineDataStore<TEnum, TTimelineEntity> TimelineDataStore { get; }

        TTimelineEntity GetTimelineEntity(TEnum name);

        PlayableDirector GetPlayableDirector(TEnum name);

    }

    public class TimelineRepository<TEnum, TTimelineEntity> : ITimelineRepository<TEnum, TTimelineEntity>
        where TEnum : struct
        where TTimelineEntity : ITimelineEntity<TEnum>, new() {

        public class Factory : DefaultRepositoryFactory<TimelineRepository<TEnum, TTimelineEntity>> {

            protected override void Initialize(TimelineRepository<TEnum, TTimelineEntity> instance) {
                base.Initialize(instance);
                instance.TimelineDataStore = new TimelineDataStore<TEnum, TTimelineEntity>.Factory().Create();
            }

        }

        [Inject]
        public ITimelineDataStore<TEnum, TTimelineEntity> TimelineDataStore { get; private set; }

        public TTimelineEntity GetTimelineEntity(TEnum name) {
            return this.TimelineDataStore.GetTimelineEntity(name);
        }

        public PlayableDirector GetPlayableDirector(TEnum name) {
            return this.TimelineDataStore.GetPlayableDirector(name);
        }

    }

}
