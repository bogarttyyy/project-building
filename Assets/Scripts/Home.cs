using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] private int[] inventoryArray = { 
        0,
        0, // Gold
        0, // Food
        0, // Metal
        0  // Wood
    };
    [SerializeField] private List<ItemContainer> inventory;

    private void Start() {
        inventory = new List<ItemContainer>()
        {
            new ItemContainer(EResourceType.Generic),
            new ItemContainer(EResourceType.Gold),
            new ItemContainer(EResourceType.Food),
            new ItemContainer(EResourceType.Metal),
            new ItemContainer(EResourceType.Wood)
        };
    }

    public void AddResource(Resource resource)
    {
        var container = inventory.FirstOrDefault(x => x.resourceType == resource.resourceType);

        if (container != null)
        {
            container.Deposit(resource.Empty());
        }
        
        inventoryArray[(int)resource.resourceType] += resource.Empty();
    }

    public Resource GetResource(EResourceType resourceType, int units){
        inventoryArray[(int)resourceType] -= units;

        //Get Container
        var container = inventory.FirstOrDefault(x => x.resourceType == resourceType);
        // Subtract from Home Container
        return new Resource(resourceType, units);
    }

    public void DepositResource(Resource resource)
    {
        var container = GetContainer(resource.resourceType);
        var amount = resource.Empty();
        container.Deposit(amount);
        Debug.Log($"NPC Resource transferred: {amount}");
        Debug.Log($"Home Container Gained: {container.GetStock()}");

        inventoryArray[(int)resource.resourceType] += resource.Empty();
    }

    private ItemContainer GetContainer(EResourceType type){
        var container = inventory.FirstOrDefault(c => c.resourceType == type);

        if (container == null)
        {
            container = new ItemContainer(type);
            inventory.Add(container);
        }

        return container;
    }
}
