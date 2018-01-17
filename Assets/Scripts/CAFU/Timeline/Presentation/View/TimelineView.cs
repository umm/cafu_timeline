using System.Collections.Generic;
using System.Linq;
using CAFU.Core.Presentation.View;
using CAFU.Timeline.Domain.Model;
using CAFU.Timeline.Domain.UseCase;
using CAFU.Timeline.Presentation.Presenter;
using UnityEngine;
using UnityEngine.Playables;

namespace CAFU.Timeline.Presentation.View {

    public interface ITimelineView {

    }

    public abstract class TimelineView<TEnum, TTimelineInformation> : MonoBehaviour, IView, ITimelineView, IPlayableDirectorResolver<TEnum>
        where TEnum : struct
        where TTimelineInformation : TimelineInformation<TEnum>, new() {

        [SerializeField]
        private List<TTimelineInformation> timelineInformationList;

        private List<TTimelineInformation> TimelineInformationList => this.timelineInformationList;

        protected abstract ITimelinePresenter<TEnum, TTimelineInformation> GetTimelinePresenter();

        private void Awake() {
            this.GetTimelinePresenter().RegisterPlayableDirectorResolver(this);
        }

        public PlayableDirector GetPlayableDirector(TEnum timelineName) {
            if (!this.TimelineInformationList.Any(x => x.Name.Equals(timelineName))) {
                this.AddTimelineInformationFromTransform(timelineName);
            }
            return this.TimelineInformationList.Find(x => x.Name.Equals(timelineName)).PlayableDirector;
        }

        private void AddTimelineInformationFromTransform(TEnum timelineName) {
            // enum のアンダースコアをスラッシュに置換して、Hierarchy を探す
            Transform playableDirectorTransform = this.transform.Find(timelineName.ToString().Replace("_", "/"));
            if (playableDirectorTransform != default(Transform)) {
                this.TimelineInformationList.Add(
                    new TTimelineInformation() {
                        Name = timelineName,
                        PlayableDirector = playableDirectorTransform.GetComponent<PlayableDirector>(),
                    }
                );
            }
        }

    }

}