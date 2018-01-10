using CAFU.Timeline.Domain.Model;
using CAFU.Timeline.Domain.UseCase;
using UnityEngine;
using UnityEngine.Playables;
using UnityModule.Playables;

namespace CAFU.Timeline.Presentation.Presenter {

    public interface ITimelinePresenter<TEnum, TTimelineInformation>
        where TEnum : struct
        where TTimelineInformation : TimelineInformation<TEnum>, new() {

        TimelineUseCase<TEnum, TTimelineInformation> TimelineUseCase { get; }

    }

    public static class TimelinePresenterExtension {

        public static void RegisterPlayableDirectorResolver<TEnum, TTimelineInformation>(this ITimelinePresenter<TEnum, TTimelineInformation> self, IPlayableDirectorResolver<TEnum> playableDirectorResolver) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() {
            self.TimelineUseCase.RegisterPlayableDirectorResolver(playableDirectorResolver);
        }

        public static PlayableDirector GetPlayableDirector<TEnum, TTimelineInformation>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() {
            return self.TimelineUseCase.GetPlayableDirector(name);
        }

        public static void SetGenericBindingByTrackName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string trackName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() where TValue: Object {
            self.GetPlayableDirector(name).SetGenericBindingByTrackName(trackName, value);
        }

        public static void SetGenericBindingByPlayableAssetName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string playableAssetName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() where TValue: Object {
            self.GetPlayableDirector(name).SetGenericBindingByPlayableAssetName(playableAssetName, value);
        }

        public static void SetGenericBindingByTrackNameAndPlayableAssetName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string trackName, string playableAssetName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() where TValue: Object {
            self.GetPlayableDirector(name).SetGenericBindingByTrackNameAndPlayableAssetName(trackName, playableAssetName, value);
        }

        public static void SetReferenceValueByTrackName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string trackName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() where TValue: Object {
            self.GetPlayableDirector(name).SetReferenceValueByTrackName(trackName, value);
        }

        public static void SetReferenceValueByPlayableAssetName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string playableAssetName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() where TValue: Object {
            self.GetPlayableDirector(name).SetReferenceValueByPlayableAssetName(playableAssetName, value);
        }

        public static void SetReferenceValueByTrackNameAndPlayableAssetName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string trackName, string playableAssetName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum>, new() where TValue: Object {
            self.GetPlayableDirector(name).SetReferenceValueByTrackNameAndPlayableAssetName(trackName, playableAssetName, value);
        }

    }

}