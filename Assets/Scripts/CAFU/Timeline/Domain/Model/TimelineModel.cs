using System.Collections.Generic;
using System.Linq;
using CAFU.Core.Domain;
using UnityEngine;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.Model {

    public class TimelineModel<TEnum> : IModel where TEnum : struct {

        public List<TimelineInformation<TEnum>> TimelineInformationList { get; private set; }

        public void RegisterTimelineInformationList<TTimelineInformation>(IEnumerable<TTimelineInformation> timelineInformationList)
            where TTimelineInformation : TimelineInformation<TEnum> {
            this.TimelineInformationList = timelineInformationList.Cast<TimelineInformation<TEnum>>().ToList();
        }

    }

    public abstract class TimelineInformation<TEnum> where TEnum : struct {

        [SerializeField]
        private TEnum name;

        public TEnum Name => this.name;

        [SerializeField]
        private PlayableDirector playableDirector;

        public PlayableDirector PlayableDirector => this.playableDirector;

    }

}