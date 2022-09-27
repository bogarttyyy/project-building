using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Home homeBase;
    [SerializeField] private List<ResourceContainer> resourceSpots;

    public static GameManager Instance { get; private set;}

    private void Awake() {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start() {
        if (resourceSpots == null)
        {
            resourceSpots = new List<ResourceContainer>();
        }
    }

    public ResourceContainer GetNearestResourceSpot(Transform currentLocation)
    {
        float currentMin = 0f;
        ResourceContainer nearestContainer = resourceSpots.FirstOrDefault();
        
        foreach (var spot in resourceSpots)
        {
            float distance = Vector3.Distance(spot.transform.position, currentLocation.position);

            if (distance < currentMin)
            {
                currentMin = distance;
                nearestContainer = spot;
            }
        }

        return nearestContainer;
    }

    public Home GetHomebase(Transform transform)
    {
        return homeBase;
    }
}
