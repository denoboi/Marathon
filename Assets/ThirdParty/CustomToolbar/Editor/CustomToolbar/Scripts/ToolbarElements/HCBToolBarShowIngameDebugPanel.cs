using System;
using UnityEngine;
using UnityEditor;
using UnityToolbarExtender;
using HCB.Core;
using HCB.UI;

[Serializable]
internal class HCBToolBarShowIngameDebugPanel : BaseToolbarElement
{
	private static GUIContent reloadSceneBtn;

	public override string NameInList => "[Button] Open HCB Debug Panel";

	public override void Init()
	{
		reloadSceneBtn = new GUIContent((Texture2D)AssetDatabase.LoadAssetAtPath($"{GetPackageRootPath}/Editor/CustomToolbar/Icons/DebuPanelButtonIcon.png", typeof(Texture2D)), "Reload scene");
	}

	protected override void OnDrawInList(Rect position)
	{

	}

	protected override void OnDrawInToolbar()
	{
		if (!Application.isPlaying)
			return;

		EditorGUIUtility.SetIconSize(new Vector2(17, 17));
		if (GUILayout.Button(reloadSceneBtn, ToolbarStyles.commandButtonStyle))
		{
			if (EditorApplication.isPlaying)
			{
				HCBPanelList.HCBPanels[HCBPanelList.DebugPanel].TogglePanel();
			}
		}
	}
}
