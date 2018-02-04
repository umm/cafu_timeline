using System;
using System.Collections.Generic;
using System.Linq;
using CAFU.Core.Presentation.View;
using CAFU.Timeline.Domain.Model;
using CAFU.Timeline.Domain.UseCase;
using CAFU.Timeline.Presentation.Presenter;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;

namespace CAFU.Timeline.Presentation.View {

    public interface ITimelineView : IView, IObservableAwakeMonoBehaviour {

    }

    public abstract class TimelineView<TTimelineModel> : ObservableLifecycleMonoBehaviour, ITimelineView, IPlayableDirectorResolver
        where TTimelineModel : class, ITimelineModel, new() {

        [SerializeField]
        private List<TTimelineModel> timelineModelList;

        private List<TTimelineModel> TimelineModelList => this.timelineModelList;

        protected abstract ITimelinePresenter<TTimelineModel> GetTimelinePresenter();

        protected override void OnAwake() {
            this.GetTimelinePresenter().RegisterPlayableDirectorResolver(this);
        }

        public PlayableDirector GetPlayableDirector<TEnum>(TEnum timelineName) where TEnum : struct {
            if (!this.TimelineModelList.OfType<TimelineModel<TEnum>>().Any(x => x.Name.Equals(timelineName))) {
                this.AddTimelineModelFromTransform(timelineName);
            }
            return this.TimelineModelList.OfType<TimelineModel<TEnum>>().ToList().Find(x => x.Name.Equals(timelineName)).PlayableDirector;
        }

        private void AddTimelineModelFromTransform<TEnum>(TEnum timelineName) where TEnum : struct {
            // enum のアンダースコアをスラッシュに置換して、Hierarchy を探す
            Transform playableDirectorTransform = this.transform.Find(timelineName.ToString().Replace("_", "/"));
            // 見付からなかった場合に、配下の全 Transform の名前を enum の完全一致で探す
            if (playableDirectorTransform == default(Transform)) {
                playableDirectorTransform = this.transform.GetComponentsInChildren<Transform>(true).ToList().Find(x => x.name == timelineName.ToString());
            }
            // それでも見付からなかった場合は Exception を throw する
            if (playableDirectorTransform == default(Transform)) {
                throw new ArgumentException($"TimelineName '{timelineName}' does not found.");
            }
            TimelineModel<TEnum> timelineModel = new TTimelineModel() as TimelineModel<TEnum>;
            if (timelineModel == null) {
                return;
            }
            timelineModel.Name = timelineName;
            timelineModel.PlayableDirector = playableDirectorTransform.GetComponent<PlayableDirector>();
            this.TimelineModelList.Add(timelineModel as TTimelineModel);
        }

    }

}