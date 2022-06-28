using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HCB.Core;

public class CountdownTimer : MonoBehaviour
{
    
    [SerializeField] private float countdownTime;
    [SerializeField] private TextMeshProUGUI countdownDisplay;

    private void Start()
    {
        CountDown();
    }

    private void OnEnable()
    {
        Events.OnCountdown.AddListener(CountDown);
    }

    private void OnDisable()
    {
        Events.OnCountdown.RemoveListener(CountDown);
    }

    private void CountDown()
    {
        StartCoroutine(CountDownToStart());
    }


    IEnumerator CountDownToStart()
    {
        while(countdownTime > 0)
        {

            GameManager.Instance.IsCountdown = true;
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;

            
            
        }

        countdownDisplay.text = "GO!";

    }
}
