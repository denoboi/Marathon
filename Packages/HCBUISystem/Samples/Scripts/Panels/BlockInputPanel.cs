using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

namespace HCB.UI
{
    public class BlockInputPanel : HCBPanel
    {
        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.AddListener(HidePanel);
            GameManager.Instance.OnStageFail.AddListener(ShowPanel);
            GameManager.Instance.OnStageSuccess.AddListener(ShowPanel);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.RemoveListener(HidePanel);
            GameManager.Instance.OnStageFail.RemoveListener(ShowPanel);
            GameManager.Instance.OnStageSuccess.RemoveListener(ShowPanel);
        }
    }
}
