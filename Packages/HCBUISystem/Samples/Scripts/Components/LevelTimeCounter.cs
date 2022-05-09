using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HCB.Core;

namespace HCB.UI
{
    public class LevelTimeCounter : MonoBehaviour
    {
        Text counterText;
        Text CounterText { get { return (counterText == null) ? counterText = GetComponent<Text>() : counterText; } }

        private float startTime = 0;
        private float timePassed = 0;

        private IEnumerator coroutine;

        private void Awake()
        {
            coroutine = StartCounting();
        }

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.AddListener(() => CounterText.text = "Not Started");
            LevelManager.Instance.OnLevelStart.AddListener(() => {
                startTime = Time.time;
                StartCoroutine(coroutine);
                });
            LevelManager.Instance.OnLevelFinish.AddListener(() =>
            {
                StopCoroutine(coroutine);
                CounterText.text = "LevelFinished: " + timePassed.ToString("f0");
                timePassed = 0;

            });
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;
            SceneController.Instance.OnSceneLoaded.AddListener(() => CounterText.text = "Not Started");
            LevelManager.Instance.OnLevelStart.RemoveListener(() => {
                startTime = Time.time;
                StartCoroutine(coroutine);
            });
            LevelManager.Instance.OnLevelFinish.RemoveListener(() => {
                StopCoroutine(coroutine);
                CounterText.text = "LevelFinished: " + timePassed.ToString("f0");
                timePassed = 0;

            });
        }

        private IEnumerator StartCounting()
        {
            timePassed = 0;
            startTime = Time.time;
            while (true)
            {
                timePassed = Mathf.Abs(startTime - Time.time);
                CounterText.text = "Play Time: " + timePassed.ToString("f0");
                yield return null;
            }
        }
    }
}
