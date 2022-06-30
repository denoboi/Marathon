using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HCB.Core;

public class CountdownTimer : MonoBehaviour
{
    private void Awake()
    {
        countdownDisplay.gameObject.SetActive(true);
    }

    [SerializeField] private float countdownTime ;
    [SerializeField] private TextMeshProUGUI countdownDisplay;
    private float currentCountDownTime;
    private Coroutine _countDownCoroutine;

    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(CountDown);
        LevelManager.Instance.OnLevelFinish.AddListener(StopCoroutine);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        LevelManager.Instance.OnLevelStart.RemoveListener(CountDown);
        LevelManager.Instance.OnLevelFinish.RemoveListener(StopCoroutine); //coroutine durdurmazsak diger levellarda da devam ediyor.
    }

    private void CountDown()
    {
        countdownDisplay.gameObject.SetActive(true);
        currentCountDownTime = countdownTime; //bu kod sadece 1 kez initialize oldugu icin tekrar atamam gerek
        StopCoroutine();
        _countDownCoroutine = StartCoroutine(CountDownToStart());
    }


    IEnumerator CountDownToStart()
    {
        yield return null;

        while (currentCountDownTime > 0)
        {
  
            countdownDisplay.text = currentCountDownTime.ToString();

            yield return new WaitForSeconds(1f);

            GameManager.Instance.IsCountdown = true; //Ust objesine koymak gerekiyordu bu scripti, kapatiyorum acamiyodum sonra.

            currentCountDownTime--;

        }

        countdownDisplay.text = "GO!";
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
