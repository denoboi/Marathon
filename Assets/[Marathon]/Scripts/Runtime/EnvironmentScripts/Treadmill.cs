using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

public class Treadmill : MonoBehaviour
{
    private MeshRenderer _renderer;


    public MeshRenderer Renderer { get { return _renderer == null ? _renderer = GetComponent<MeshRenderer>() : _renderer; } }

    private void OnEnable()
    {
        Events.OnOffsetPositive.AddListener(MaterialOffsetPositive);
        Events.OnOffsetNegative.AddListener(MaterialOffsetNegative);
    }

    private void OnDisable()
    {
        Events.OnOffsetPositive.RemoveListener(MaterialOffsetPositive);
        Events.OnOffsetNegative.RemoveListener(MaterialOffsetNegative);
    }


    void MaterialOffsetPositive()
    {
        float offset = Time.time * 0.1f;
        Renderer.materials[1].mainTextureOffset = new Vector2(offset,0);
    }

    void MaterialOffsetNegative()
    {
        float offset = Time.time * -0.1f;
        Renderer.materials[1].mainTextureOffset = new Vector2(offset, 0);
    }
}
