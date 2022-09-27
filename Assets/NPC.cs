using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private ItemContainer container;

    public bool hasResource;

    private void Awake() {
        container = new ItemContainer(EResourceType.Generic);
    }

    public void AddToContainer(Resource resource){
        AddToContainer(resource.resourceType, resource.Empty());
    }

    public void AddToContainer(EResourceType type, int units){
        container.resourceType = type;
        container.Deposit(units);
        hasResource = true;
    }

    public Resource EmptyContainer(){
        return new Resource(container.resourceType, container.Empty());
    }
}
