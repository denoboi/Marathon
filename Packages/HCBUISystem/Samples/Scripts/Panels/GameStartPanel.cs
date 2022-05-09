using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

namespace HCB.UI
{
    public class GameStartPanel : HCBPanel
    {
        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            LevelManager.Instance.OnLevelStart.AddListener(HidePanel);
            SceneController.Instance.OnSceneLoaded.AddListener(ShowPanel);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            LevelManager.Instance.OnLevelStart.RemoveListener(HidePanel);
            SceneController.Instance.OnSceneLoaded.RemoveListener(ShowPanel);
        }
    }
}
