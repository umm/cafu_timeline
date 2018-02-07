using CAFU.Core.Domain.UseCase;
using CAFU.Timeline.Data.Entity;
using CAFU.Timeline.Domain.Repository;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.UseCase {

    public interface ITimelineUseCase : IUseCase {

        PlayableDirector GetPlayableDirector<TEnum>(TEnum name) where TEnum : struct;

    }

    public class TimelineUseCase<TTimelineEntity> : ITimelineUseCase
        where TTimelineEntity : ITimelineEntity {

        public class Factory : DefaultUseCaseFactory<Factory, TimelineUseCase<TTimelineEntity>> {

            protected override void Initialize(TimelineUseCase<TTimelineEntity> instance) {
                base.Initialize(instance);
                instance.TimelineRepository = TimelineRepository<TTimelineEntity>.Factory.Instance.Create();
            }

        }

        private ITimelineRepository TimelineRepository { get; set; }

        public PlayableDirector GetPlayableDirector<TEnum>(TEnum name) where TEnum : struct {
            return this.TimelineRepository.GetPlayableDirector(name);
        }

    }

}