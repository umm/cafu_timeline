using CAFU.Timeline.Domain.Repository;
using CAFU.Core.Domain.Repository;
using CAFU.Core.Domain.UseCase;
using CAFU.Timeline.Data.Entity;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.UseCase {

    public class TimelineUseCase<TTimelineEntity> : IUseCase
        where TTimelineEntity : class, ITimelineEntity, new() {

        private ITimelineRepository TimelineRepository { get; }

        public TimelineUseCase() {
            // FIXME: Use Zenject
            this.TimelineRepository = new DefaultRepositoryFactory<TimelineRepository<TTimelineEntity>>().Create();
        }

        public PlayableDirector GetPlayableDirector<TEnum>(TEnum name) where TEnum : struct {
            return this.TimelineRepository.GetPlayableDirector(name);
        }

    }

}