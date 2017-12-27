using System.Collections.Generic;
using CAFU.Core.Presentation;
using CAFU.Timeline.Domain.Model;
using CAFU.Timeline.Domain.UseCase;
using UniRx.Triggers;
using UnityEngine.Playables;
using UnityModule.Playables;

namespace CAFU.Timeline.Presentation.Presenter {

    public interface ITimelinePresenter<TEnum, in TTimelineInformation> : IPresenter
        where TEnum : struct
        where TTimelineInformation : TimelineInformation<TEnum> {

        TimelineUseCase<TEnum> TimelineUseCase { get; }

        void InitializeTimelineInformationList(IEnumerable<TTimelineInformation> timelineInformationList);

    }

    public static class TimelinePresenterExtension {

        public static PlayableDirector GetPlayableDirector<TEnum, TTimelineInformation>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum> {
            return self.TimelineUseCase.GetPlayableDirector(name);
        }

        public static ObservableTimeControlTrigger GetObservableTimeControlTrigger<TEnum, TTimelineInformation>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum> {
            return self.TimelineUseCase.GetObservableTimeControlTrigger(name);
        }

        public static void SetGenericBindingByPlayableAssetName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string playableAssetName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum> where TValue: UnityEngine.Object {
            self.GetPlayableDirector(name).SetGenericBindingByPlayableAssetName(playableAssetName, value);
        }

        public static void SetGenericBindingByTrackName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string trackName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum> where TValue: UnityEngine.Object {
            self.GetPlayableDirector(name).SetGenericBindingByTrackName(trackName, value);
        }

        public static void SetGenericBindingByTrackNameAndPlayableAssetName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string trackName, string playableAssetName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum> where TValue: UnityEngine.Object {
            self.GetPlayableDirector(name).SetGenericBindingByTrackNameAndPlayableAssetName(trackName, playableAssetName, value);
        }

        public static void SetReferenceValueByPlayableAssetName<TEnum, TTimelineInformation, TValue>(this ITimelinePresenter<TEnum, TTimelineInformation> self, TEnum name, string playableAssetName, TValue value) where TEnum : struct where TTimelineInformation : TimelineInformation<TEnum> where TValue: UnityEngine.Object {
            self.GetPlayableDirector(name).SetReferenceValueByPlayableAssetName(playableAssetName, value);
        }

    }

}