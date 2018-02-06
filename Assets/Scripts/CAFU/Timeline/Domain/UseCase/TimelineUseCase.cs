using CAFU.Core.Domain.UseCase;
using CAFU.Timeline.Domain.Repository;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.UseCase {

    public class TimelineUseCase<TEnum> : IUseCase
        where TEnum : struct {

        public class Factory : DefaultUseCaseFactory<Factory, TimelineUseCase<TEnum>> {

            protected override void Initialize(TimelineUseCase<TEnum> instance) {
                base.Initialize(instance);
                instance.TimelineRepository = TimelineRepository<TEnum>.Factory.Instance.Create();
            }

        }

        private ITimelineRepository<TEnum> TimelineRepository { get; set; }

        public PlayableDirector GetPlayableDirector(TEnum name) {
            return this.TimelineRepository.GetPlayableDirector(name);
        }

    }

}