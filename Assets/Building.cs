using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    [SerializeField]
    private int progress = 0;
    
    [SerializeField]
    private int total = 3;

    [SerializeField]
    private Material[] materials;

    public EResourceType resourceMaterial;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBuilding();
    }

    private void UpdateBuilding()
    {
        if (IsBuilt())
        {
            rend.sharedMaterial = materials[1];
            EventManager.BuildingFinishedEvent();
        }
    }

    public bool IsBuilt(){
        return progress >= total;
    }

    public void Build(Resource resource)
    {
        progress += resource.Empty();
    }
}
