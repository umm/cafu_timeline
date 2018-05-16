using System.IO;
using System.Linq;
using CAFU.Generator;
using CAFU.Generator.Enumerates;
using JetBrains.Annotations;
using UnityEditor;

namespace CAFU.Timeline.Generator
{
    [UsedImplicitly]
    public class TimelineName : ClassStructureBase
    {
        private const string StructureName = "Application/Enumerate/TimelineName";

        public override string Name { get; } = StructureName;

        protected override ParentLayerType ParentLayerType { get; } = ParentLayerType.Application;

        protected override LayerType LayerType { get; } = LayerType.Enumerate;

        protected override string ModuleName { get; } = "umm@cafu_timeline";

        private int CurrentSceneNameIndex { get; set; }

        public TimelineName()
        {
        }

        public TimelineName(int currentSceneNameIndex)
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

            var generator = new ScriptGenerator(parameter, CreateTemplatePath(TemplateType.Class, StructureName));

            generator.Generate(CreateOutputPath(parameter));
        }

        protected override string CreateNamespace(Parameter parameter)
        {
            return $"{this.CreateNamespacePrefix()}{ParentLayerType.ToString()}.{LayerType.ToString()}.TimelineName";
        }

        protected override string CreateOutputPath(Parameter parameter)
        {
            return Path.Combine(UnityEngine.Application.dataPath, OutputDirectory, parameter.ParentLayerType.ToString(), parameter.LayerType.ToString(), "TimelineName", $"{parameter.ClassName}{ScriptExtension}");
        }
    }
}