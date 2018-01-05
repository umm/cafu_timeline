using System.Collections.Generic;
using System.Linq;
using CAFU.Core.Domain;
using CAFU.Timeline.Domain.Model;
using UniRx.Triggers;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.UseCase {

    public class TimelineUseCase<TEnum> : IUseCase, IUseCaseBuilder
        where TEnum : struct {

        private TimelineModel<TEnum> TimelineModel { get; set; }

        public void Build() {
            this.TimelineModel = new TimelineModel<TEnum>();
        }

        public void RegisterTimelineInformationList<TTimelineInformation>(IEnumerable<TTimelineInformation> timelineInformationList)
            where TTimelineInformation : TimelineInformation<TEnum> {
            this.TimelineModel.RegisterTimelineInformationList(timelineInformationList.Cast<TimelineInformation<TEnum>>().ToList());
        }

        public PlayableDirector GetPlayableDirector(TEnum name) {
            return this.TimelineModel.TimelineInformationList.Find(x => Equals(x.Name, name)).PlayableDirector;
        }

    }

}