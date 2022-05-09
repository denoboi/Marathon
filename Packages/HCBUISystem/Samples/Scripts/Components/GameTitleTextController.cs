using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HCB.UI
{
    public class GameTitleTextController : MonoBehaviour
    {
        TextMeshProUGUI gameTitleText;
        TextMeshProUGUI GameTitleText { get { return (gameTitleText == null) ? gameTitleText = GetComponent<TextMeshProUGUI>() : gameTitleText; } }

        private void Start()
        {
            GameTitleText.SetText(Application.productName);
        }
    }
}
