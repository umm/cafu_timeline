using System;
using System.Collections.Generic;
using System.Linq;
using CAFU.Timeline.Domain.Model;
using CAFU.Timeline.Domain.UseCase;
using CAFU.Timeline.Presentation.Presenter;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;

namespace CAFU.Timeline.Presentation.View {

    [Obsolete("Please use TimelineView<TTimelineModel> instead of this class.")]
    public abstract class TimelineView<TEnum, TTimelineInformation> : MonoBehaviour, ITimelineView, IPlayableDirectorResolver<TEnum>
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
            // 見付からなかった場合に、配下の全 Transform の名前を enum の完全一致で探す
            if (playableDirectorTransform == default(Transform)) {
                playableDirectorTransform = this.transform.GetComponentsInChildren<Transform>(true).ToList().Find(x => x.name == timelineName.ToString());
            }
            if (playableDirectorTransform != default(Transform)) {
                this.TimelineInformationList.Add(
                    new TTimelineInformation() {
                        Name = timelineName,
                        PlayableDirector = playableDirectorTransform.GetComponent<PlayableDirector>(),
                    }
                );
            }
        }

        public UniRx.IObservable<Unit> OnAwakeAsObservable() {
            throw new NotImplementedException();
        }

    }

}