using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HCB.UI.Editor
{
    public class UIComponentCreator : MonoBehaviour
    {
        [MenuItem("GameObject/UI/HCB Panel", false, 0)]
        public static void CreatePanel()
        {
            CreateComponent(CreatePanel);
        }

        static void CreatePanel(Transform parent)
        {
            GameObject hcbpanel = new GameObject();
            hcbpanel.transform.SetParent(parent);
            hcbpanel.AddComponent<RectTransform>();
            hcbpanel.gameObject.name = "HCBPanel";
            hcbpanel.AddComponent<HCBPanel>();

            RectTransform panelRect = hcbpanel.GetComponent<RectTransform>();
            panelRect.SetAnchor(AnchorPresets.StretchAll);
            panelRect.anchoredPosition = Vector3.zero;
            panelRect.sizeDelta = Vector2.zero;
            panelRect.localScale = Vector3.one;
        }


        static void CreateComponent(UnityAction<Transform> action)
        {
            GameObject selectedObject = Selection.activeGameObject;

            if (selectedObject)
            {
                RectTransform rectTransform = selectedObject.GetComponent<RectTransform>();
                if (rectTransform)
                    action.Invoke(selectedObject.transform);
                else
                {
                    Canvas canvas = GameObject.FindObjectOfType<Canvas>();

                    if (canvas)
                    {
                        action.Invoke(canvas.transform);
                    }
                    else
                        action.Invoke(CreateCanvas());
                }
            }
            else
            {
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                if (canvas)
                    action.Invoke(canvas.transform);
                else
                    action.Invoke(CreateCanvas());
            }

            UnityEngine.EventSystems.EventSystem eventSystem = GameObject.FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
            if (eventSystem == null)
            {
                GameObject eventSystemObj = new GameObject();
                eventSystemObj.name = "Event System";
                eventSystemObj.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            }
        }

        static Transform CreateCanvas()
        {
            GameObject canvasObject = new GameObject();
            canvasObject.gameObject.name = "Canvas";
            canvasObject.AddComponent<RectTransform>();
            Canvas canvas = canvasObject.GetComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(480, 800);

            return canvasObject.transform;
        }
    }
}
