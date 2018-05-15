using CAFU.Generator;
using CAFU.Generator.Enumerates;
using UnityEditor;

namespace CAFU.Timeline.GeneratorExtension
{
    [InitializeOnLoad]
    public class TimelinePresenter : IClassStructureExtension
    {
        private bool ImplementsTimelinePresenter { get; set; }

        static TimelinePresenter()
        {
            var instance = new TimelinePresenter();
            GeneratorWindow.RegisterAdditionalOptionRenderDelegate(LayerType.Controller, instance);
            GeneratorWindow.RegisterAdditionalOptionRenderDelegate(LayerType.Presenter, instance);
            GeneratorWindow.RegisterAdditionalStructureExtensionDelegate(LayerType.Presenter, instance);
        }

        public void OnGUI()
        {
            ImplementsTimelinePresenter = EditorGUILayout.Toggle("Implements ITimelinePresenter?", ImplementsTimelinePresenter);
        }

        public void Process(Parameter parameter)
        {
            if (ImplementsTimelinePresenter)
            {
                parameter.UsingList.Add("CAFU.Timeline.Presentation.Presenter");
                parameter.UsingList.Add("CAFU.Timeline.Domain.UseCase");
                parameter.UsingList.Add($"TimelineName = {GeneratorWindow.ProjectContext.NamespacePrefix}.Application.Enumerates.TimelineName.Title");
                parameter.UsingList.Add($"TimelineEntity = {GeneratorWindow.ProjectContext.NamespacePrefix}.Data.Entity.TimelineEntity.Title");
                parameter.ImplementsInterfaceList.Add("ITimelinePresenter<TimelineName>");
                parameter.PropertyList.Add(
                    new Parameter.Property()
                    {
                        Accessibility = Accessibility.Public,
                        Interface = "ITimelineUseCase<TimelineName>",
                        Type = "TimelineUseCase<TimelineName, TimelineEntity>",
                        Name = "TimelineUseCase",
                    }
                );
            }
        }
    }
}