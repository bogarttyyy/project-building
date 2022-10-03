using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    // public NavMeshSurface surface;
    [SerializeField] private Home homeBase;
    [SerializeField] private List<ResourceContainer> resourceSpots;

    public Camera cam;
    [SerializeField] private Building selectedBuilding;
    public List<Building> buildingList {get; private set;}

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
        EventManager.OnDebugEvent += HandleOnDebugEvent;

        if (resourceSpots == null)
        {
            resourceSpots = new List<ResourceContainer>();
        }

        if (buildingList == null)
        {
            buildingList = new List<Building>();
        }
    }

    private void Update() {
        HandleOnBuildClick();
    }

    private void HandleOnBuildClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                // selectedBuilding.transform.position = hit.point;
                Building build = Instantiate(selectedBuilding);
                build.transform.position = new Vector3(hit.point.x, 1, hit.point.z);
                AddBuilding(build);
            }
        }
    }

    private void AddBuilding(Building build)
    {
        buildingList.Add(build);
        EventManager.NewBuildingPlottedEvent(build);
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

    private void HandleOnDebugEvent(){
        Debug.Log("Fire Debug Event!");
    }

    internal Building GetStructureToBuild(Transform currentLocation)
    {
        float currentMin = 0f;
        Building nearestBuilding = buildingList.FirstOrDefault();
        
        foreach (var building in buildingList)
        {
            if (building.IsBuilt())
            {
                float distance = Vector3.Distance(building.transform.position, currentLocation.position);

                if (distance < currentMin)
                {
                    currentMin = distance;
                    nearestBuilding = building;
                }
            }
        }

        return nearestBuilding;
    }
}
