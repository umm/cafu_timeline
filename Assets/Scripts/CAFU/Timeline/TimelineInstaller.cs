using CAFU.Timeline.Data.DataStore;
using CAFU.Timeline.Data.Entity;
using CAFU.Timeline.Domain.Repository;
using CAFU.Timeline.Domain.UseCase;
using Zenject;

namespace CAFU.Timeline {

    // ReSharper disable once ClassNeverInstantiated.Global
    public class TimelineInstaller<TEnum, TTimelineEntity, TTimelineDataStore> : Installer<TimelineInstaller<TEnum, TTimelineEntity, TTimelineDataStore>>
        where TEnum : struct
        where TTimelineEntity : ITimelineEntity<TEnum>, new()
        where TTimelineDataStore : ITimelineDataStore<TEnum, TTimelineEntity> {

        public override void InstallBindings() {
            this.Container.Bind<ITimelineUseCase<TEnum, TTimelineEntity>>().To<TimelineUseCase<TEnum, TTimelineEntity>>().AsTransient();
            this.Container.Bind<ITimelineRepository<TEnum, TTimelineEntity>>().To<TimelineRepository<TEnum, TTimelineEntity>>().AsTransient();
//            this.Container.Bind<ITimelineDataStore<TEnum, TTimelineEntity>>().To<TTimelineDataStore>().AsTransient();
        }

    }

}