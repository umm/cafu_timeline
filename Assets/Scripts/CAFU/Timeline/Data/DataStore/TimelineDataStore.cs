using CAFU.Core.Data.DataStore;
using CAFU.Timeline.Data.Entity;
using UnityEngine.Playables;

namespace CAFU.Timeline.Data.DataStore {

    public interface ITimelineDataStore : IDataStore {

        PlayableDirector GetPlayableDirector<TEnum>(TEnum timelineName) where TEnum : struct;

    }

}