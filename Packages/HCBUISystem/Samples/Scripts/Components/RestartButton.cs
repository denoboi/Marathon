using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using HCB.Core;

namespace HCB.UI
{
    public class RestartButton : Button
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            RestartLevel();
        }

        private void RestartLevel()
        {
            GameManager.Instance.CompeleteStage(false);
        }
    }
}
