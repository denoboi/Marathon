using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HCB.Core;

public class EliminatedUI : MonoBehaviour
{
  
    private void OnEnable()
    {
        Events.OnEliminated.AddListener(Tween);
        LevelManager.Instance.OnLevelFinish.AddListener(() => { transform.localScale = Vector3.zero; });
    }
    private void OnDisable()
    {
        Events.OnEliminated.RemoveListener(Tween);
        LevelManager.Instance.OnLevelFinish.RemoveListener(() => { transform.localScale = Vector3.zero; });
    }

    public void Tween()
    {
        transform.DOScale(Vector3.one * 1.2f, 1f).SetEase(Ease.OutQuart).OnComplete(() => { GameManager.Instance.CompeleteStage(false); });
    }
    
}
