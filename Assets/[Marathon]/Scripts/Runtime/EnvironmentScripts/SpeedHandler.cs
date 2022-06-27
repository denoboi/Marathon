using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HCB.Core;

public class SpeedHandler : MonoBehaviour
{
    public Slider Slider;
    public TreadmillController TreadmillController;

    

    private void OnSceneLoad()
    {
 
        Slider = GetComponent<Slider>();
        TreadmillController = FindObjectOfType<TreadmillController>(); 
        Slider.maxValue = TreadmillController.MaxSpeed;
        Slider.minValue = 0;

    }

    private void OnEnable()
    {
        SceneController.Instance.OnSceneLoaded.AddListener(OnSceneLoad);
    }


    private void OnDisable()
    {
        SceneController.Instance.OnSceneLoaded.RemoveListener(OnSceneLoad);
    }


    private void Update()
    {
        if (Slider == null)
            return;

        Slider.value = TreadmillController.Speed;
    }
}
