using System.Linq;
using CAFU.Generator;
using CAFU.Generator.Enumerates;
using JetBrains.Annotations;
using UnityEditor;

namespace CAFU.Timeline.Generator
{
    [UsedImplicitly]
    public class TimelineScripts : ClassStructureBase
    {
        private const string StructureName = "ScriptSet/Timeline";

        public override string Name { get; } = StructureName;

        protected override string ModuleName { get; } = "umm@cafu_timeline";

        private int CurrentSceneNameIndex { get; set; }

        public override void OnGUI()
        {
            base.OnGUI();
            CurrentSceneNameIndex = EditorGUILayout.Popup("Scene Name", CurrentSceneNameIndex, GeneratorWindow.SceneNameList.ToArray());
        }

        public override void Generate(bool overwrite)
        {
            new TimelineEntity(CurrentSceneNameIndex).Generate(overwrite);
            new TimelineName(CurrentSceneNameIndex).Generate(overwrite);
            new TimelineDataStore(CurrentSceneNameIndex).Generate(overwrite);
        }

        protected override ParentLayerType ParentLayerType { get; } = ParentLayerType.Other;
        protected override LayerType LayerType { get; } = LayerType.Other;
    }
}