using System;
using CAFU.Timeline.Data.Entity;

namespace CAFU.Timeline.Data.DataStore.Scene {

    [Obsolete("Use `CAFU.Timeline.Data.DataStore.TimelineDataStore` instead of this class.")]
    public abstract class TimelineDataStore<TEnum, TTimelineEntity> : DataStore.TimelineDataStore<TEnum, TTimelineEntity>
        where TEnum : struct
        where TTimelineEntity : ITimelineEntity {
    }

}