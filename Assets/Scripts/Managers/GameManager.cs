using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const int STRUCTURE_LAYER = 7;

    // public NavMeshSurface surface;
    [SerializeField] private Camera cam;
    [SerializeField] private Mode mode;
    [SerializeField] private Home homeBase;
    [SerializeField] private List<ResourceContainer> resourceSpots;
    [SerializeField] private Building selectedBuilding;
    [SerializeField] private float buildRotationSensitivity = 0.1f;

    private Building currentBuilding;

    public List<Building> buildingList {get; private set;}
    public List<Building> structuresToBeBuilt { get; private set; }

    public static GameManager Instance { get; private set;}

    private enum Mode
    {
        Play,
        Build
    }

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

        mode = Mode.Play;

        SetupBuildingEvents();

        if (resourceSpots == null)
        {
            resourceSpots = new List<ResourceContainer>();
        }
    }
    
    

    private void Update()
    {
        // Press R to reset stage
        Reset();
        
        // Press B to change modes (Play/Build)
        ModeChange();

        // Handle build logic
        BuildLogic();
    }

    private void Reset()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentBuilding?.gameObject?.SetActive(false);
            mode = Mode.Play;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void ModeChange()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (currentBuilding == null)
            {
                currentBuilding = Instantiate(selectedBuilding);
            }
            else
            {
                currentBuilding.gameObject.SetActive(true);
            }

            mode = Mode.Build;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            currentBuilding?.gameObject?.SetActive(false);
            mode = Mode.Play;
        }
    }

    private void BuildLogic()
    {
        if (mode == Mode.Build)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && !Input.GetMouseButton(1) && !Input.GetMouseButtonUp(1))
            {
                currentBuilding.transform.position = new Vector3(hit.point.x, 1, hit.point.z);
            }

            HandleOnBuildClick(currentBuilding);
        }
    }

    private void SetupBuildingEvents()
    {
        EventManager.OnBuildingFinishedEvent += HandleOnBuildingFinishedEvent;

        if (buildingList == null)
        {
            buildingList = new List<Building>();
        }

        if (structuresToBeBuilt == null)
        {
            structuresToBeBuilt = new List<Building>();
        }
    }

    private void HandleOnBuildClick(Building building)
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 currentScreenPos = cam.WorldToScreenPoint(building.transform.position);
            //building.transform.position = new Vector3(plotPosition.x, 1, plotPosition.z);

            float adjustment = (currentScreenPos.x - Input.mousePosition.x) * buildRotationSensitivity;

            building.transform.localEulerAngles = new Vector3(0, adjustment, 0);

            if (!building.isValid)
            {
                Debug.Log("Invalid Placement");
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (!building.isValid)
            {
                Debug.Log("Cannot Place Building");
            }
            else
            {
                AddBuilding(building);
            }
        }
    }

    private void AddBuilding(Building build)
    {
        buildingList.Add(build);
        UpdateStructuresToBuild();
        EventManager.NewBuildingPlottedEvent(build);

        currentBuilding = Instantiate(selectedBuilding);
    }

    private void HandleOnBuildingFinishedEvent(Building building)
    {
        UpdateStructuresToBuild();
    }

    private void UpdateStructuresToBuild()
    {
        structuresToBeBuilt = buildingList.Where(b => !b.IsBuilt()).ToList();
    }

    public Building GetNearestUnbuiltStructure(Transform currentLocation)
    {
        float currentMin = 0f;
        Building nearestBuilding = structuresToBeBuilt.FirstOrDefault();

        foreach (var building in structuresToBeBuilt)
        {
            float distance = Vector3.Distance(building.transform.position, currentLocation.position);

            if (distance < currentMin)
            {
                currentMin = distance;
                nearestBuilding = building;
            }
        }

        return nearestBuilding;
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

    public List<Building> GetStructuresToBuild()
    {
        return structuresToBeBuilt;
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

    private void OnDestroy()
    {
        EventManager.OnBuildingFinishedEvent -= HandleOnBuildingFinishedEvent;
    }
}
