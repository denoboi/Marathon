using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowTween : MonoBehaviour
{
    void Start()
    {
        transform.DOLocalMoveY(-0.18f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
}
