using CAFU.Core.Domain.UseCase;
using CAFU.Timeline.Domain.Model;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.UseCase {

    public interface IPlayableDirectorResolver {

        PlayableDirector GetPlayableDirector<TEnum>(TEnum timelineName) where TEnum : struct;

    }

    public class TimelineUseCase<TTimelineModel> : IUseCase, IUseCaseFactory<TimelineUseCase<TTimelineModel>>
        where TTimelineModel : ITimelineModel, new() {

        private IPlayableDirectorResolver PlayableDirectorResolver { get; set; }

        public void RegisterPlayableDirectorResolver(IPlayableDirectorResolver playableDirectorResolver) {
            this.PlayableDirectorResolver = playableDirectorResolver;
        }

        public PlayableDirector GetPlayableDirector<TEnum>(TEnum name) where TEnum : struct {
            return this.PlayableDirectorResolver.GetPlayableDirector(name);
        }

        public TimelineUseCase<TTimelineModel> Create() {
            return new TimelineUseCase<TTimelineModel>();
        }

    }

}