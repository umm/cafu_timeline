using CAFU.Core.Data.Entity;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Playables;

namespace CAFU.Timeline.Data.Entity
{
    [PublicAPI]
    public interface ITimelineEntity : IEntity
    {
    }

    [PublicAPI]
    public interface ITimelineEntity<out TEnum> : ITimelineEntity where TEnum : struct
    {
        TEnum Name { get; }

        PlayableDirector PlayableDirector { get; }
    }

    [PublicAPI]
    public class TimelineEntity<TEnum> : ITimelineEntity<TEnum> where TEnum : struct
    {
        [SerializeField] private TEnum name;

        public TEnum Name
        {
            get { return name; }
            set { name = value; }
        }

        [SerializeField] private PlayableDirector playableDirector;

        public PlayableDirector PlayableDirector
        {
            get { return playableDirector; }
            set { playableDirector = value; }
        }
    }
}