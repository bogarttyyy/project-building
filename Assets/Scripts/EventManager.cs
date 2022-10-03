using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<Building> OnNewBuildingPlottedEvent;

    public static event Action OnDebugEvent;

    private void Update() {
        HandleOnDebugEvent();
    }

    private void HandleOnDebugEvent(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDebugEvent?.Invoke();        
        }
    }

    internal static void NewBuildingPlottedEvent(Building building)
    {
        OnNewBuildingPlottedEvent?.Invoke(building);
    }
}
