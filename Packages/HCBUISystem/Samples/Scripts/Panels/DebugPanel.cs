using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HCB.Core;
using TMPro;

namespace HCB.UI
{
    public class DebugPanel : HCBPanel
    {

        public Text TimeScaleText;

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

           InputManager.Instance.OnTouch.AddListener(ToogleOnCount);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            InputManager.Instance.OnTouch.AddListener(ToogleOnCount);
        }

        public void ToogleOnCount(int value)
        {
            if (value == 3)
                TogglePanel();
        }

        public void LoadNextLevel()
        {
            LevelManager.Instance.LoadNextLevel();
        }

        public void LoadPreviousLevel()
        {
            LevelManager.Instance.LoadPreviousLevel();
        }

        public void RestartLevel()
        {
            LevelManager.Instance.ReloadLevel();
        }

        public void CompilateStage(bool value)
        {
            GameManager.Instance.CompeleteStage(value);
        }

        public void SetTimeScale(float value)
        {
            Time.timeScale = value;
            TimeScaleText.text = Time.timeScale.ToString();
        }

        public void IncreaseTimeScale()
        {
            Time.timeScale += 0.5f;
            TimeScaleText.text = Time.timeScale.ToString();
        }

        public void DecreaseTimeScale()
        {
            Time.timeScale -= 0.5f;
            TimeScaleText.text = Time.timeScale.ToString();
        }

        public void ResetTime()
        {

            Time.timeScale = 1f;
            TimeScaleText.text = Time.timeScale.ToString();
        }
    }
}
