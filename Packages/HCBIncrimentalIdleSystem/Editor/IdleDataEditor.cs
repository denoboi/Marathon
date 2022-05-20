using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HCB.Core;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace HCB.IncrimantalIdleSystem.Editor
{
    public class IdleDataEditor : OdinEditorWindow
    {
        [InlineEditor(InlineEditorModes.GUIOnly)]
        public List<IdleStat> IdleStats = new List<IdleStat>();

        [MenuItem("HyperCasualBase/Idle Data Editor")]
        private static void OpenWindow()
        {
            GetWindow<IdleDataEditor>().Show();
        }

        protected override void OnEnable()
        {
            Initialize();
        }

        [Button]
        protected override void Initialize()
        {
            base.Initialize();
            IdleStats.Clear();

            string[] paths = AssetDatabase.FindAssets("t:IdleStat");

            for (int i = 0; i < paths.Length; i++)
            {
                IdleStats.Add(AssetDatabase.LoadAssetAtPath<IdleStat>(AssetDatabase.GUIDToAssetPath(paths[i])));
            }
        }

        
    }
}
