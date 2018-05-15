using CAFU.Core.Domain.UseCase;
using CAFU.Timeline.Domain.Repository;
using JetBrains.Annotations;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.UseCase
{
    public interface ITimelineUseCase<in TEnum> : IUseCase where TEnum : struct
    {
        PlayableDirector GetPlayableDirector(TEnum name);
    }

    [PublicAPI]
    public class TimelineUseCase<TEnum> : ITimelineUseCase<TEnum>
        where TEnum : struct
    {
        public class Factory : DefaultUseCaseFactory<TimelineUseCase<TEnum>>
        {
            protected override void Initialize(TimelineUseCase<TEnum> instance)
            {
                base.Initialize(instance);
                instance.TimelineRepository = new TimelineRepository<TEnum>.Factory().Create();
            }
        }

        private ITimelineRepository<TEnum> TimelineRepository { get; set; }

        public PlayableDirector GetPlayableDirector(TEnum name)
        {
            return TimelineRepository.GetPlayableDirector(name);
        }
    }
}