using CAFU.Core.Data.DataStore;
using CAFU.Timeline.Data.Entity;
using UnityEngine.Playables;

namespace CAFU.Timeline.Data.DataStore {

    public interface ITimelineDataStore<in TEnum, TTimelineEntity> : IDataStore where TEnum : struct where TTimelineEntity : ITimelineEntity {

        PlayableDirector GetPlayableDirector(TEnum timelineName);

    }

}