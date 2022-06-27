using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Events
{
    //Use this event manager for your custom ingame events.
    public static UnityEvent OnEliminated = new UnityEvent();
    public static UnityEvent OnStaminaLow = new UnityEvent();
    public static UnityEvent OnStaminaNormal = new UnityEvent();
    public static UnityEvent OnOffsetPositive = new UnityEvent();
    public static UnityEvent OnOffsetNegative = new UnityEvent();
    public static UnityEvent OnPlayerFall = new UnityEvent();
    public static UnityEvent OnShaking = new UnityEvent();
    public static UnityEvent OnStageComplete = new UnityEvent();


}
