using UnityEngine;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.Model {

    public interface ITimelineModel {

    }

    public abstract class TimelineModel<TEnum> : ITimelineModel where TEnum : struct {

        [SerializeField]
        private TEnum name;

        public TEnum Name {
            get {
                return this.name;
            }
            set {
                this.name = value;
            }
        }

        [SerializeField]
        private PlayableDirector playableDirector;

        public PlayableDirector PlayableDirector {
            get {
                return this.playableDirector;
            }
            set {
                this.playableDirector = value;
            }
        }

    }

}