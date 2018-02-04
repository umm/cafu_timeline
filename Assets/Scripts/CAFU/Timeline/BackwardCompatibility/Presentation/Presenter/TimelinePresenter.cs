using System;
using CAFU.Timeline.Domain.Model;
using CAFU.Timeline.Domain.UseCase;
using UnityEngine.Playables;
using UnityModule.Playables;
using Object = UnityEngine.Object;
#pragma warning disable 618

namespace CAFU.Timeline.Presentation.Presenter {

    [Obsolete("Please use ITimelinePresenter<TTimelineModel> instead of this interface.")]
    public interface ITimelinePresenter<TEnum, TTimelineInformation>
        where TEnum : struct
        where TTimelineInformation : TimelineInformation<TEnum>, new() {

        TimelineUseCase<TEnum, TTimelineInformation> TimelineUseCase { get; }

    }

    public static class TimelinePresenterExtensionBackwardCompatibility {

        [Obsolete("Please use RegisterPlayableDirectorResolver<TTimelineModel>(this ITimelinePresenter<TTimelineModel> presenter) instead of this extension method.")]
        public static void RegisterPlayableDirectorResolver<TEnum, TTimelineInformation>(this ITimelinePresenter<TEnum, TTimelineInformation> self, IPlayableDirectorResolver<TEnum> playableDirectorResolver) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() {
            self.TimelineUseCase.RegisterPlayableDirectorResolver(playableDirectorResolver);
        }

        [Obsolete("Please use GetPlayableDirector<TEnum, TTimelineModel>(this ITimelinePresenter<TTimelineModel> presenter, TEnum name) instead of this extension method.")]
        public static PlayableDirector GetPlayableDirector<TEnum, TTimelineInformation>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() {
            return self.TimelineUseCase.GetPlayableDirector(name);
        }

        [Obsolete("Please use SetGenericBindingByTrackName<TEnum, TTimelineModel, TValue>(this ITimelinePresenter<TTimelineModel> presenter, TEnum name, string trackName, TValue value) instead of this extension method.")]
        public static void SetGenericBindingByTrackName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string trackName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() where TValue: Object {
            self.GetPlayableDirector(name).SetGenericBindingByTrackName(trackName, value);
        }

        [Obsolete("Please use SetGenericBindingByPlayableAssetName<TEnum, TTimelineModel, TValue>(this ITimelinePresenter<TTimelineModel> presenter, TEnum name, string playableAssetName, TValue value) instead of this extension method.")]
        public static void SetGenericBindingByPlayableAssetName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string playableAssetName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() where TValue: Object {
            self.GetPlayableDirector(name).SetGenericBindingByPlayableAssetName(playableAssetName, value);
        }

        [Obsolete("Please use SetGenericBindingByPlayableAssetName<TEnum, TTimelineModel, TValue>(this ITimelinePresenter<TTimelineModel> presenter, TEnum name, string trackName, string playableAssetName, TValue value) instead of this extension method.")]
        public static void SetGenericBindingByTrackNameAndPlayableAssetName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string trackName, string playableAssetName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() where TValue: Object {
            self.GetPlayableDirector(name).SetGenericBindingByTrackNameAndPlayableAssetName(trackName, playableAssetName, value);
        }

        [Obsolete("Please use SetReferenceValueByTrackName<TEnum, TTimelineModel, TValue>(this ITimelinePresenter<TTimelineModel> presenter, TEnum name, string trackName, TValue value) instead of this extension method.")]
        public static void SetReferenceValueByTrackName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string trackName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() where TValue: Object {
            self.GetPlayableDirector(name).SetReferenceValueByTrackName(trackName, value);
        }

        [Obsolete("Please use SetReferenceValueByPlayableAssetName<TEnum, TTimelineModel, TValue>(this ITimelinePresenter<TTimelineModel> presenter, TEnum name, string playableAssetName, TValue value) instead of this extension method.")]
        public static void SetReferenceValueByPlayableAssetName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string playableAssetName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() where TValue: Object {
            self.GetPlayableDirector(name).SetReferenceValueByPlayableAssetName(playableAssetName, value);
        }

        [Obsolete("Please use SetReferenceValueByTrackNameAndPlayableAssetName<TEnum, TTimelineModel, TValue>(this ITimelinePresenter<TTimelineModel> presenter, TEnum name, string trackName, string playableAssetName, TValue value) instead of this extension method.")]
        public static void SetReferenceValueByTrackNameAndPlayableAssetName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string trackName, string playableAssetName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() where TValue: Object {
            self.GetPlayableDirector(name).SetReferenceValueByTrackNameAndPlayableAssetName(trackName, playableAssetName, value);
        }

    }

}