using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HCB.UI;

public class ScaleTween : MonoBehaviour
{
    void Start()
    {
        transform.DOScale(Vector3.one * 1.2f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
}
