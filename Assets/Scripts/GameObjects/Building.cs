using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    private const int STRUCTURE_LAYER = 7;

    [SerializeField]
    private int progress = 0;
    
    [SerializeField]
    private int total = 3;

    private BuildingState state;
    public bool isValid = true;

    [SerializeField]
    private Material[] materials;
    
    [SerializeField] private TMP_Text buildingText;

    public EResourceType resourceMaterial;
    private Renderer rend;
    private NavMeshObstacle navMeshObstacle;

    private enum BuildingState
    {
        Blueprint,
        Unbuilt,
        Built,
        Destroyed
    }

    void Start()
    {
        SetupEvents();

        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.enabled = false;

        state = BuildingState.Blueprint;
        
        buildingText.text = $"Progress: {progress} / {total}";
    }

    private void SetupEvents()
    {
        EventManager.OnNewBuildingPlottedEvent += HandlePlottedEvent;
    }

    private void HandlePlottedEvent(Building building)
    {
        if (this == building)
        {
            state = BuildingState.Unbuilt;
        }
    }

    void Update()
    {
        UpdateBuilding();
    }

    private void UpdateBuilding()
    {
        if (IsBuilt())
        {
            state = BuildingState.Built;
            rend.sharedMaterial = materials[1];
            navMeshObstacle.enabled = true;
            buildingText.text = "Building";
            EventManager.BuildingFinishedEvent(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == BuildingState.Blueprint)
        {
            if (other.gameObject.layer == GameManager.STRUCTURE_LAYER)
            {
                isValid = false;
                rend.sharedMaterial = materials[2];
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (state == BuildingState.Blueprint)
        {
            if (other.gameObject.layer == GameManager.STRUCTURE_LAYER)
            {
                isValid = true;
                rend.sharedMaterial = materials[0];
            }
        }
    }

    public bool IsBuilt(){
        return progress >= total;
    }

    public void Build(Resource resource)
    {
        progress += resource.Empty();
        Debug.Log(progress);
        buildingText.text = $"Progress: {progress} / {total}";
    }
}
