using CAFU.Core.Domain.UseCase;
using CAFU.Timeline.Data.Entity;
using CAFU.Timeline.Domain.Repository;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.UseCase {

    public interface ITimelineUseCase<in TEnum, TTimelineEntity> : IUseCase where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum> {

        ITimelineRepository<TEnum, TTimelineEntity> TimelineRepository { get; }

        PlayableDirector GetPlayableDirector(TEnum name);

    }

    public class TimelineUseCase<TEnum, TTimelineEntity> : ITimelineUseCase<TEnum, TTimelineEntity>
        where TEnum : struct
        where TTimelineEntity : ITimelineEntity<TEnum> {

        public class Factory : DefaultUseCaseFactory<TimelineUseCase<TEnum, TTimelineEntity>> {

            protected override void Initialize(TimelineUseCase<TEnum, TTimelineEntity> instance) {
                base.Initialize(instance);
                instance.TimelineRepository = new TimelineRepository<TEnum, TTimelineEntity>.Factory().Create();
            }

        }

        public ITimelineRepository<TEnum, TTimelineEntity> TimelineRepository { get; private set; }

        public PlayableDirector GetPlayableDirector(TEnum name) {
            return this.TimelineRepository.GetPlayableDirector(name);
        }

    }

}