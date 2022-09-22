using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] private List<ItemContainer> inventory;

    private void Start() {
        inventory = new List<ItemContainer>()
        {
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
    }

    public Resource GetResource(EResourceType resourceType, int units){

        var container = inventory.FirstOrDefault(x => x.resourceType == resourceType);

        return new Resource(resourceType, container.Withdraw(units));
    }
}
