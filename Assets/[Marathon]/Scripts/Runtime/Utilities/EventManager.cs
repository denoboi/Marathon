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
}
