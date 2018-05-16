using System.IO;
using System.Linq;
using CAFU.Generator;
using CAFU.Generator.Enumerates;
using JetBrains.Annotations;
using UnityEditor;

namespace CAFU.Timeline.Generator
{
    [UsedImplicitly]
    public class TimelineEntity : ClassStructureBase
    {
        private const string StructureName = "Data/Entity/TimelineEntity";

        public override string Name { get; } = StructureName;

        protected override ParentLayerType ParentLayerType { get; } = ParentLayerType.Data;

        protected override LayerType LayerType { get; } = LayerType.Entity;

        protected override string ModuleName { get; } = "umm@cafu_timeline";

        private int CurrentSceneNameIndex { get; set; }

        public TimelineEntity()
        {
        }

        public TimelineEntity(int currentSceneNameIndex)
        {
            CurrentSceneNameIndex = currentSceneNameIndex;
        }

        public override void OnGUI()
        {
            base.OnGUI();
            CurrentSceneNameIndex = EditorGUILayout.Popup("Scene Name", CurrentSceneNameIndex, GeneratorWindow.SceneNameList.ToArray());
        }

        public override void Generate(bool overwrite)
        {
            var parameter = new Parameter()
            {
                ParentLayerType = ParentLayerType,
                LayerType = LayerType,
                ClassName = GeneratorWindow.SceneNameList[CurrentSceneNameIndex],
                SceneName = GeneratorWindow.SceneNameList[CurrentSceneNameIndex],
                Overwrite = overwrite,
            };
            parameter.Namespace = CreateNamespace(parameter);

            parameter.UsingList.Add("System");
            parameter.UsingList.Add("CAFU.Timeline.Data.Entity");
            parameter.UsingList.Add($"TimelineName = {this.CreateNamespacePrefix()}Application.Enumerate.TimelineName.{parameter.SceneName}");
            parameter.BaseClassName = "TimelineEntity<TimelineName>";

            var generator = new ScriptGenerator(parameter, CreateTemplatePath(TemplateType.Class, StructureName));

            generator.Generate(CreateOutputPath(parameter));
        }

        protected override string CreateNamespace(Parameter parameter)
        {
            return $"{this.CreateNamespacePrefix()}{ParentLayerType.ToString()}.{LayerType.ToString()}.TimelineEntity";
        }

        protected override string CreateOutputPath(Parameter parameter)
        {
            return Path.Combine(UnityEngine.Application.dataPath, OutputDirectory, parameter.ParentLayerType.ToString(), parameter.LayerType.ToString(), "TimelineEntity", $"{parameter.ClassName}{ScriptExtension}");
        }
    }
}