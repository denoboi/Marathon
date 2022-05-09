using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

namespace HCB.UI
{
    public class TutorialPanel : HCBPanel
    {
        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.AddListener(ShowPanel);
            LevelManager.Instance.OnLevelStart.AddListener(HidePanel);
        }



        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.RemoveListener(ShowPanel);
            LevelManager.Instance.OnLevelStart.RemoveListener(HidePanel);
        }
    }
}
