using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Package
{
    [PreviewField]
    [TableColumnWidth(70, Resizable = false)]
    public Sprite Icon;
    [VerticalGroup("Strings")]
    public string Name = "HCB";
    [VerticalGroup("Strings")]
    public string Link;

    [TableColumnWidth(100, Resizable = false)]
    [Button("Import")]
    private void Import()
    {
        throw new System.NotImplementedException("Import package function haven't been implimented.");
    }

}

public class PackagesData : ScriptableObject
{
    [TableList]
    public List<Package> Packages = new List<Package>();
}
