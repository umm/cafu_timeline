using System.Collections.Generic;
using CAFU.Core.Presentation.View;
using CAFU.Timeline.Domain.Model;
using UnityEngine;

namespace CAFU.Timeline.Presentation.View {

    public interface ITimelineView {

    }

    public abstract class TimelineView<TEnum, TTimelineInformation> : MonoBehaviour, IView, ITimelineView
        where TEnum : struct
        where TTimelineInformation : TimelineInformation<TEnum> {

        [SerializeField]
        private List<TTimelineInformation> timelineInformationList;

        protected IEnumerable<TTimelineInformation> TimelineInformationList => this.timelineInformationList;

    }

}