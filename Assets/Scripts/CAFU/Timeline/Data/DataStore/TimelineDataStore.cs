using CAFU.Core.Data.DataStore;
using UnityEngine.Playables;

namespace CAFU.Timeline.Data.DataStore {

    public interface ITimelineDataStore : IDataStore {

    }

    public interface ITimelineDataStore<in TEnum> : ITimelineDataStore where TEnum : struct {

        PlayableDirector GetPlayableDirector(TEnum timelineName);

    }

}