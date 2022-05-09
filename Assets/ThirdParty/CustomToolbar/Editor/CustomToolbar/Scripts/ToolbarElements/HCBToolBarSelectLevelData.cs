using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using HCB.Core;
using HCB.Utilities;
using HCB;

[Serializable]
internal class HCBToolBarSelectLevelData : BaseToolbarElement
{
	public override string NameInList => "[Dropdown] Level Data selection";

	[SerializeField] bool showDataFolder = true;

    LevelGuiData[] levelDataPopupDisplay;
    private string[] levelDataPaths;
    private int selectedDataIndex;

    public override void Init()
    {
        base.Init();
        RefreshDataList();
    }

   

    protected override void OnDrawInList(Rect position)
    {
        position.width = 200.0f;
        showDataFolder = EditorGUI.Toggle(position, "Group by folders", showDataFolder);
    }

    protected override void OnDrawInToolbar()
    {
        EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
        DrawDataDropdown();
        EditorGUI.EndDisabledGroup();
    }

    private void DrawDataDropdown()
    {
        selectedDataIndex = EditorGUILayout.Popup(selectedDataIndex, levelDataPopupDisplay.Select(e => e.popupDisplay).ToArray(), GUILayout.Width(WidthInToolbar));

        if (GUI.changed && 0 <= selectedDataIndex && selectedDataIndex < levelDataPopupDisplay.Length)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                foreach (var dataPath in levelDataPaths)
                {
                    if ((dataPath) == levelDataPopupDisplay[selectedDataIndex].path)
                    {
                        Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(dataPath);
                    }
                }
            }
        }
    }

    private void RefreshDataList()
	{
		List<LevelGuiData> toDisplay = new List<LevelGuiData>();

		selectedDataIndex = -1;


		string[] levelDataGuids = AssetDatabase.FindAssets("t:LevelData", new string[] { "Assets", "Packages" });
		levelDataPaths = new string[levelDataGuids.Length];
		for (int i = 0; i < levelDataPaths.Length; ++i)
		{
			levelDataPaths[i] = AssetDatabase.GUIDToAssetPath(levelDataGuids[i]);
		}

		for (int i = 0; i < levelDataPaths.Length; ++i)
		{
			string name;

			if (showDataFolder)
			{
				string folderName = Path.GetFileName(Path.GetDirectoryName(levelDataPaths[i]));
				name = $"{folderName}/{GetDataName(levelDataPaths[i])}";
			}
			else
			{
				name = GetDataName(levelDataPaths[i]);
			}

			GUIContent content = new GUIContent(name, "Open scene");

			toDisplay.Add(new LevelGuiData()
			{
				path = levelDataPaths[i],
				popupDisplay = content,
			});

		}

		levelDataPopupDisplay = toDisplay.ToArray();
	}

    string GetDataName(string path)
    {
        path = path.Replace(".asset", "");
        return Path.GetFileName(path);
    }

    class LevelGuiData
    {
        public string path;
        public GUIContent popupDisplay;
    }
}
