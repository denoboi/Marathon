using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HCB.Core;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CountDownTimerText : MonoBehaviour
{
    [SerializeField] Ease easeType;

    private Tween _tween;


    private void Awake()
    {
        countdownDisplay.gameObject.SetActive(true);
    }

    //[SerializeField] private string countdownTime;
    [SerializeField] private TextMeshProUGUI countdownDisplay;

    private Coroutine _countDownCoroutine;
    private bool isCounting = true;

    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(CountDown);
        LevelManager.Instance.OnLevelFinish.AddListener(StopCoroutine);
        SceneController.Instance.OnSceneLoaded.AddListener(() => isCounting = true);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        LevelManager.Instance.OnLevelStart.RemoveListener(CountDown);
        LevelManager.Instance.OnLevelFinish.RemoveListener(StopCoroutine); //coroutine durdurmazsak diger levellarda da devam ediyor.
        SceneController.Instance.OnSceneLoaded.RemoveListener(() => isCounting = true);
    }

    private void CountDown()
    {
        countdownDisplay.gameObject.SetActive(true);
        isCounting = true;
        StopCoroutine();

        _countDownCoroutine = StartCoroutine(CountDownToStart(2));
    }


    IEnumerator CountDownToStart(float waitTime)
    {
        yield return null;


        while(isCounting)
        {

            

            GameManager.Instance.IsCountdown = true;
            
            countdownDisplay.text = "Ready";
            _tween = countdownDisplay.gameObject.transform.DOScale(Vector3.one * 2f, 1f).SetEase(easeType);
            DOTween.Kill(_tween);

            yield return new WaitForSeconds(waitTime);

            

            isCounting = false;

            countdownDisplay.text = "Set";
            _tween.Play();

            yield return new WaitForSeconds(waitTime);

           
            
        }

        countdownDisplay.text = "GO!";
        countdownDisplay.gameObject.transform.DOScale(Vector3.forward, 1f).SetEase(easeType);
        GameManager.Instance.IsCountdown = false;
        
        EventManager.OnCountDownEnd.Invoke();

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);

    }

    private void StopCoroutine()
    {
        if (_countDownCoroutine != null)
        {
            StopCoroutine(_countDownCoroutine);
        }

    }
}