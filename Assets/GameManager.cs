using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject homeBase;
    [SerializeField] private List<ResourceContainer> resourceSpots;

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
}
