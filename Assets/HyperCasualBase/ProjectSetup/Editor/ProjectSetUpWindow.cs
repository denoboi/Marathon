using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Facebook.Unity.Settings;
using System.Linq;

public enum Company { None, HyperboxGames, KaijuGames, Gamejon }

[InitializeOnLoad]
public class ProjectSetUpWindow : OdinEditorWindow
{
    [BoxGroup("Setup")]
    [OnValueChanged("SetUpKickOffList")]
    public Company CompanyName = Company.None;
    [OnValueChanged("SetUpKickOffList")]
    [BoxGroup("Setup")]
    public string GameName = string.Empty;
    [BoxGroup("Setup")]
    [OnValueChanged("SetUpKickOffList")]
    public string bundleName = string.Empty;
    [BoxGroup("Setup")]
    [OnValueChanged("SetUpKickOffList")]
    [ReadOnly]
    public string bundleID = "com.companyname";
    [BoxGroup("Setup")]
    [OnValueChanged("SetUpKickOffList")]
    public string FacebookAppID;
    [OnValueChanged("SetUpKickOffList")]
    [BoxGroup("Setup")]
    [OnValueChanged("SetUpKickOffList")]
    public string ElephantGameID;
    [OnValueChanged("SetUpKickOffList")]
    [BoxGroup("Setup")]
    public string ElephantGameSecret;

    [InlineEditor(InlineEditorModes.GUIOnly)]
    public PackagesData PackagesData;

    public ElephantSettings ElephantSettings { get { return
                (ElephantSettings)AssetDatabase.LoadAssetAtPath("Assets/Resources/ElephantSettings.asset", typeof(ElephantSettings));
        } }

    protected override void OnEnable()
    {
        base.OnEnable();
        PackagesData = (PackagesData)AssetDatabase.LoadAssetAtPath("Assets/HyperCasualBase/ProjectSetup/Data/HCBPakages.asset", typeof(PackagesData));
        saveChangesMessage = "There are empty fields. Please fill them";
        LoadInfo();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        SaveInfo();
    }

    private void SetUpKickOffList()
    {
        bundleID = "com." + CompanyName.ToString().ToLower() + "." + bundleName.ToLower();
        bundleID = bundleID.Replace(" ", "");
        PlayerSettings.companyName = CompanyName.ToString();
        PlayerSettings.productName = GameName;
        ElephantSettings.GameID = ElephantGameID;
        ElephantSettings.GameSecret = ElephantGameSecret;
        EditorUtility.SetDirty(ElephantSettings);
        if(!string.IsNullOrEmpty(FacebookAppID))
            FacebookSettings.AppIds[0] = FacebookAppID;

        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, bundleID);
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, bundleID);

        switch (CompanyName)
        {
            case Company.None:
                break;
            case Company.HyperboxGames:
                PlayerSettings.iOS.appleDeveloperTeamID = "URJGDDD72C";
                break;
            case Company.KaijuGames:
                PlayerSettings.iOS.appleDeveloperTeamID = "6N5893AVU9";
                break;
            case Company.Gamejon:
                PlayerSettings.iOS.appleDeveloperTeamID = "7HVT3A86L6";
                break;
            default:
                break;
        }
    }

    private bool CanClose()
    {
        if (CompanyName == Company.None)
            return true;

        if (string.IsNullOrEmpty(GameName))
            return true;

        if (string.IsNullOrEmpty(FacebookAppID))
            return true;

        if (string.IsNullOrEmpty(ElephantGameID))
            return true;

        if (string.IsNullOrEmpty(ElephantGameSecret))
            return true;

        return false;
    }

    private void SaveInfo()
    {
        EditorPrefs.SetInt("compName", (int)CompanyName);
        EditorPrefs.SetString("gameName", GameName);
        EditorPrefs.SetString("bundleId", bundleID);
        EditorPrefs.SetString("facebookId", FacebookAppID);
        EditorPrefs.SetString("eGameId", ElephantGameID);
        EditorPrefs.SetString("eGameSecret", ElephantGameSecret);
    }

    private void LoadInfo()
    {
        CompanyName = (Company)EditorPrefs.GetInt("compName", 0);
        GameName = EditorPrefs.GetString("gameName", "ExampleGame");
        bundleID = EditorPrefs.GetString("bundleId", "com." + CompanyName.ToString().ToLower() + "." + GameName.ToLower());
        FacebookAppID = EditorPrefs.GetString("facebookId", FacebookAppID);
        ElephantGameID = EditorPrefs.GetString("eGameId", ElephantGameID);
        ElephantGameSecret = EditorPrefs.GetString("eGameSecret", ElephantGameSecret);
    }

    public override void SaveChanges()
    {
        base.SaveChanges();
        SaveInfo();
    }

    [MenuItem("Debug/ProjectSetup")]
    static void DebugShow()
    {
        ProjectSetUpWindow window = (ProjectSetUpWindow)GetWindow(typeof(ProjectSetUpWindow));
        window.Show();
    }
}
