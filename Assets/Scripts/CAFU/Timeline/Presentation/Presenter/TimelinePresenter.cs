using CAFU.Core.Presentation.Presenter;
using CAFU.Timeline.Data.Entity;
using CAFU.Timeline.Domain.UseCase;
using UnityEngine;
using UnityEngine.Playables;
using UnityModule.Playables;

namespace CAFU.Timeline.Presentation.Presenter {

    public interface ITimelinePresenter<in TEnum, TTimelineEntity> : IPresenter where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum> {

        ITimelineUseCase<TEnum, TTimelineEntity> TimelineUseCase { get; }

    }

    public static class TimelinePresenterExtension {

        public static PlayableDirector GetPlayableDirector<TEnum, TTimelineEntity>(this ITimelinePresenter<TEnum, TTimelineEntity> presenter, TEnum timelineName) where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum> {
            return presenter.TimelineUseCase.GetPlayableDirector(timelineName);
        }

        public static void SetGenericBindingByTrackName<TEnum, TTimelineEntity, TValue>(this ITimelinePresenter<TEnum, TTimelineEntity> presenter, TEnum name, string trackName, TValue value) where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum> where TValue: Object {
            presenter.GetPlayableDirector(name).SetGenericBindingByTrackName(trackName, value);
        }

        public static void SetGenericBindingByPlayableAssetName<TEnum, TTimelineEntity, TValue>(this ITimelinePresenter<TEnum, TTimelineEntity> presenter, TEnum name, string playableAssetName, TValue value) where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum> where TValue: Object {
            presenter.GetPlayableDirector(name).SetGenericBindingByPlayableAssetName(playableAssetName, value);
        }

        public static void SetGenericBindingByTrackNameAndPlayableAssetName<TEnum, TTimelineEntity, TValue>(this ITimelinePresenter<TEnum, TTimelineEntity> presenter, TEnum name, string trackName, string playableAssetName, TValue value) where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum> where TValue: Object {
            presenter.GetPlayableDirector(name).SetGenericBindingByTrackNameAndPlayableAssetName(trackName, playableAssetName, value);
        }

        public static void SetReferenceValueByTrackName<TEnum, TTimelineEntity, TValue>(this ITimelinePresenter<TEnum, TTimelineEntity> presenter, TEnum name, string trackName, TValue value) where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum> where TValue: Object {
            presenter.GetPlayableDirector(name).SetReferenceValueByTrackName(trackName, value);
        }

        public static void SetReferenceValueByPlayableAssetName<TEnum, TTimelineEntity, TValue>(this ITimelinePresenter<TEnum, TTimelineEntity> presenter, TEnum name, string playableAssetName, TValue value) where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum> where TValue: Object {
            presenter.GetPlayableDirector(name).SetReferenceValueByPlayableAssetName(playableAssetName, value);
        }

        public static void SetReferenceValueByTrackNameAndPlayableAssetName<TEnum, TTimelineEntity, TValue>(this ITimelinePresenter<TEnum, TTimelineEntity> presenter, TEnum name, string trackName, string playableAssetName, TValue value) where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum> where TValue: Object {
            presenter.GetPlayableDirector(name).SetReferenceValueByTrackNameAndPlayableAssetName(trackName, playableAssetName, value);
        }

    }

}