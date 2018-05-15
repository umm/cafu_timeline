﻿using System;
using CAFU.Core.Presentation.Presenter;
using CAFU.Timeline.Data.Entity;
using CAFU.Timeline.Domain.UseCase;
using JetBrains.Annotations;
using UnityEngine.Playables;
using UnityModule.Playables;
using Object = UnityEngine.Object;

namespace CAFU.Timeline.Presentation.Presenter
{
    public interface ITimelinePresenter<in TEnum> : IPresenter where TEnum : struct
    {
        ITimelineUseCase<TEnum> TimelineUseCase { get; }
    }

    [Obsolete("Use `ITimelinePresenter<TEnum> instead of this interface.`")]
    // ReSharper disable once UnusedTypeParameter
    // ReSharper disable once UnusedMember.Global
    public interface ITimelinePresenter<in TEnum, TTimelineEntity> : ITimelinePresenter<TEnum> where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum>
    {
    }

    [PublicAPI]
    public static class TimelinePresenterExtension
    {
        public static PlayableDirector GetPlayableDirector<TEnum>(this ITimelinePresenter<TEnum> presenter, TEnum timelineName) where TEnum : struct
        {
            return presenter.TimelineUseCase.GetPlayableDirector(timelineName);
        }

        public static void SetGenericBindingByTrackName<TEnum, TValue>(this ITimelinePresenter<TEnum> presenter, TEnum name, string trackName, TValue value) where TEnum : struct where TValue : Object
        {
            presenter.GetPlayableDirector(name).SetGenericBindingByTrackName(trackName, value);
        }

        public static void SetGenericBindingByPlayableAssetName<TEnum, TValue>(this ITimelinePresenter<TEnum> presenter, TEnum name, string playableAssetName, TValue value) where TEnum : struct where TValue : Object
        {
            presenter.GetPlayableDirector(name).SetGenericBindingByPlayableAssetName(playableAssetName, value);
        }

        public static void SetGenericBindingByTrackNameAndPlayableAssetName<TEnum, TValue>(this ITimelinePresenter<TEnum> presenter, TEnum name, string trackName, string playableAssetName, TValue value) where TEnum : struct where TValue : Object
        {
            presenter.GetPlayableDirector(name).SetGenericBindingByTrackNameAndPlayableAssetName(trackName, playableAssetName, value);
        }

        public static void SetReferenceValueByTrackName<TEnum, TValue>(this ITimelinePresenter<TEnum> presenter, TEnum name, string trackName, TValue value) where TEnum : struct where TValue : Object
        {
            presenter.GetPlayableDirector(name).SetReferenceValueByTrackName(trackName, value);
        }

        public static void SetReferenceValueByPlayableAssetName<TEnum, TValue>(this ITimelinePresenter<TEnum> presenter, TEnum name, string playableAssetName, TValue value) where TEnum : struct where TValue : Object
        {
            presenter.GetPlayableDirector(name).SetReferenceValueByPlayableAssetName(playableAssetName, value);
        }

        public static void SetReferenceValueByTrackNameAndPlayableAssetName<TEnum, TValue>(this ITimelinePresenter<TEnum> presenter, TEnum name, string trackName, string playableAssetName, TValue value) where TEnum : struct where TValue : Object
        {
            presenter.GetPlayableDirector(name).SetReferenceValueByTrackNameAndPlayableAssetName(trackName, playableAssetName, value);
        }
    }
}