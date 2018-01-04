using System.Collections.Generic;
using CAFU.Core.Presentation.View;
using CAFU.Timeline.Domain.Model;
using CAFU.Timeline.Presentation.Presenter;
using UnityEngine;

namespace CAFU.Timeline.Presentation.View {

    public interface ITimelineView<TEnum, in TTimelineInformation>
        where TEnum : struct
        where TTimelineInformation : TimelineInformation<TEnum>{

        ITimelinePresenter<TEnum, TTimelineInformation> GetTimelinePresenter();

    }

    public abstract class TimelineView<TEnum, TTimelineInformation> : MonoBehaviour, IView, ITimelineView<TEnum, TTimelineInformation>
        where TEnum : struct
        where TTimelineInformation : TimelineInformation<TEnum> {

        [SerializeField]
        private List<TTimelineInformation> timelineInformationList;

        private IEnumerable<TTimelineInformation> TimelineInformationList => this.timelineInformationList;

        public abstract ITimelinePresenter<TEnum, TTimelineInformation> GetTimelinePresenter();

        private void Awake() {
            this.GetTimelinePresenter().InitializeTimelineInformationList(this.TimelineInformationList);
        }

    }

}