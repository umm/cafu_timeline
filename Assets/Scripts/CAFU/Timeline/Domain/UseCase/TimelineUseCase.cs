using CAFU.Core.Domain.UseCase;
using CAFU.Timeline.Domain.Repository;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.UseCase {

    public class TimelineUseCase<TEnum> : IUseCase
        where TEnum : struct {

        public class Factory : IUseCaseFactory<TimelineUseCase<TEnum>> {

            public TimelineUseCase<TEnum> Create() {
                return new TimelineUseCase<TEnum>() {
                    TimelineRepository = new TimelineRepository<TEnum>.Factory().Create(),
                };
            }

        }

        private ITimelineRepository<TEnum> TimelineRepository { get; set; }

        public PlayableDirector GetPlayableDirector(TEnum name) {
            return this.TimelineRepository.GetPlayableDirector(name);
        }

    }

}