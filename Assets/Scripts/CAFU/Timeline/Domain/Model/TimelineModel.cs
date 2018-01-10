using System.Collections.Generic;
using System.Linq;
using CAFU.Core.Domain;
using UnityEngine;
using UnityEngine.Playables;

namespace CAFU.Timeline.Domain.Model {

    public class TimelineModel<TEnum, TTimelineInformation> : IModel
        where TEnum : struct
        where TTimelineInformation : TimelineInformation<TEnum>, new() {

        private readonly List<TimelineInformation<TEnum>> TimelineInformationList;

        public TimelineModel() {
            this.TimelineInformationList = new List<TimelineInformation<TEnum>>();
        }

        public bool HasPlayableDirector(TEnum name) {
            return this.TimelineInformationList.Any(x => x.Name.Equals(name) && x.PlayableDirector != default(PlayableDirector));
        }

        public PlayableDirector GetPlayableDirector(TEnum name) {
            return this.TimelineInformationList.Find(x => x.Name.Equals(name)).PlayableDirector;
        }

        public void SetTimelineInformation(TEnum name, PlayableDirector playableDirector) {
            this.TimelineInformationList.Add(
                new TTimelineInformation() {
                    Name = name,
                    PlayableDirector = playableDirector,
                }
            );
        }

    }

    public abstract class TimelineInformation<TEnum> where TEnum : struct {

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