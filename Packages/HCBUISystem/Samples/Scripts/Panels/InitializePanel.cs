using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

namespace HCB.UI
{
    public class InitializePanel : HCBPanel
    {
        public HCBPanel HyperboxPanel;
        public HCBPanel KaijuPanel;
        public HCBPanel GamejonPanel;

        private void Start()
        {
            ShowPanel();

            string companyName = Application.companyName;

            if (companyName.Contains("Hyperbox"))
                HyperboxPanel.ShowPanel();
            else if (companyName.Contains("Kaiju"))
                KaijuPanel.ShowPanel();
            else if (companyName.Contains("Gamejon"))
                GamejonPanel.ShowPanel();
        }

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.AddListener(HidePanel);
            
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.RemoveListener(HidePanel);
        }
    }
}
