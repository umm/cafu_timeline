using CAFU.Core.Presentation.Presenter;
using CAFU.Timeline.Domain.Model;
using CAFU.Timeline.Domain.UseCase;
using UnityEngine;
using UnityEngine.Playables;
using UnityModule.Playables;

namespace CAFU.Timeline.Presentation.Presenter {

    public interface ITimelinePresenter<TTimelineModel> : IPresenter
        where TTimelineModel : ITimelineModel, new() {

        TimelineUseCase<TTimelineModel> TimelineUseCase { get; }

    }

    public static class TimelinePresenterExtension {

        public static void RegisterPlayableDirectorResolver<TTimelineModel>(this ITimelinePresenter<TTimelineModel> self, IPlayableDirectorResolver playableDirectorResolver) where TTimelineModel : ITimelineModel, new() {
            self.TimelineUseCase.RegisterPlayableDirectorResolver(playableDirectorResolver);
        }

        public static PlayableDirector GetPlayableDirector<TEnum, TTimelineModel>(this ITimelinePresenter<TTimelineModel> self, TEnum name) where TEnum : struct where TTimelineModel : ITimelineModel, new() {
            return self.TimelineUseCase.GetPlayableDirector(name);
        }

        public static void SetGenericBindingByTrackName<TEnum, TTimelineModel, TValue>(this ITimelinePresenter<TTimelineModel> self, TEnum name, string trackName, TValue value) where TEnum : struct where TTimelineModel : ITimelineModel, new() where TValue: Object {
            self.GetPlayableDirector(name).SetGenericBindingByTrackName(trackName, value);
        }

        public static void SetGenericBindingByPlayableAssetName<TEnum, TTimelineModel, TValue>(this ITimelinePresenter<TTimelineModel> self, TEnum name, string playableAssetName, TValue value) where TEnum : struct where TTimelineModel : ITimelineModel, new() where TValue: Object {
            self.GetPlayableDirector(name).SetGenericBindingByPlayableAssetName(playableAssetName, value);
        }

        public static void SetGenericBindingByTrackNameAndPlayableAssetName<TEnum, TTimelineModel, TValue>(this ITimelinePresenter<TTimelineModel> self, TEnum name, string trackName, string playableAssetName, TValue value) where TEnum : struct where TTimelineModel : ITimelineModel, new() where TValue: Object {
            self.GetPlayableDirector(name).SetGenericBindingByTrackNameAndPlayableAssetName(trackName, playableAssetName, value);
        }

        public static void SetReferenceValueByTrackName<TEnum, TTimelineModel, TValue>(this ITimelinePresenter<TTimelineModel> self, TEnum name, string trackName, TValue value) where TEnum : struct where TTimelineModel : ITimelineModel, new() where TValue: Object {
            self.GetPlayableDirector(name).SetReferenceValueByTrackName(trackName, value);
        }

        public static void SetReferenceValueByPlayableAssetName<TEnum, TTimelineModel, TValue>(this ITimelinePresenter<TTimelineModel> self, TEnum name, string playableAssetName, TValue value) where TEnum : struct where TTimelineModel : ITimelineModel, new() where TValue: Object {
            self.GetPlayableDirector(name).SetReferenceValueByPlayableAssetName(playableAssetName, value);
        }

        public static void SetReferenceValueByTrackNameAndPlayableAssetName<TEnum, TTimelineModel, TValue>(this ITimelinePresenter<TTimelineModel> self, TEnum name, string trackName, string playableAssetName, TValue value) where TEnum : struct where TTimelineModel : ITimelineModel, new() where TValue: Object {
            self.GetPlayableDirector(name).SetReferenceValueByTrackNameAndPlayableAssetName(trackName, playableAssetName, value);
        }

    }

}