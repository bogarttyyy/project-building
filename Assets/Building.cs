using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    private int progress = 0;
    
    [SerializeField]
    private int total = 10;

    public EResourceType resourceMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsBuilt(){
        return progress < total;
    }

    public void Build(Resource resource)
    {
        progress += resource.Empty();
    }
}
