using System;
using CAFU.Timeline.Data.Entity;

namespace CAFU.Timeline.Data.DataStore.Scene {

    [Obsolete("Please use CAFU.Timeline.Data.DataStore.TimelineDataStore<TEnum, TTimelineEntity> instead of this class.")]
    public abstract class TimelineDataStore<TEnum, TTimelineEntity> : DataStore.TimelineDataStore<TEnum, TTimelineEntity> where TEnum : struct where TTimelineEntity : ITimelineEntity<TEnum>, new() {

    }

}