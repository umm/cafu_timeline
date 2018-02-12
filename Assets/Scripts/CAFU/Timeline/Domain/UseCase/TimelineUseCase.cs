using CAFU.Core.Domain.UseCase;
using CAFU.Timeline.Data.Entity;
using CAFU.Timeline.Domain.Repository;
using UnityEngine.Playables;
using Zenject;

namespace CAFU.Timeline.Domain.UseCase {

    public interface ITimelineUseCase<in TEnum, out TTimelineEntity> : IUseCase where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum> {

        ITimelineRepository<TEnum, TTimelineEntity> TimelineRepository { get; }

        TTimelineEntity GetTimelineEntity(TEnum name);

        PlayableDirector GetPlayableDirector(TEnum name);

    }

    public class TimelineUseCase<TEnum, TTimelineEntity> : ITimelineUseCase<TEnum, TTimelineEntity>
        where TEnum : struct
        where TTimelineEntity : ITimelineEntity<TEnum>, new() {

        public class Factory : DefaultUseCaseFactory<TimelineUseCase<TEnum, TTimelineEntity>> {

            protected override void Initialize(TimelineUseCase<TEnum, TTimelineEntity> instance) {
                base.Initialize(instance);
                instance.TimelineRepository = new TimelineRepository<TEnum, TTimelineEntity>.Factory().Create();
            }

        }

        [Inject]
        public ITimelineRepository<TEnum, TTimelineEntity> TimelineRepository { get; private set; }

        public TTimelineEntity GetTimelineEntity(TEnum name) {
            return this.TimelineRepository.GetTimelineEntity(name);
        }

        public PlayableDirector GetPlayableDirector(TEnum name) {
            return this.TimelineRepository.GetPlayableDirector(name);
        }

    }

}
