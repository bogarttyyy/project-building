using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField]
    private EResourceType resourceType = EResourceType.Generic;
    [SerializeField]
    private int itemCount = 0;

    public Building building;

    public bool hasResource;

    private ItemContainer container;

    private void Awake()
    {
        container = new ItemContainer(EResourceType.Generic);
    }

    private void Start()
    {
        EventManager.OnNewBuildingPlottedEvent += BuildingPlotted;
    }

    private void BuildingPlotted(Building building)
    {
        this.building = building;
    }

    public void AddToInventory(Resource resource)
    {
        AddToContainer(resource.resourceType, resource.Empty());
    }

    public void AddToContainer(EResourceType type, int units)
    {
        container.resourceType = type;
        container.Deposit(units);

        resourceType = container.resourceType;
        itemCount = container.GetStock();

        if (itemCount > 0)
        {
            hasResource = true;
        }
    }

    public Resource GiveItem()
    {
        ResetInventory();
        itemCount = 0;
        return new Resource(container.resourceType, container.Empty());
    }

    public Resource ItemHeld()
    {
        return new Resource(container.resourceType, container.GetStock());
    }

    public void DropItem()
    {
        // IMPORTANT!!! Temporary Drop
        itemCount = 0;
        container.Empty();
    }

    private void ResetInventory()
    {
        container.resourceType = EResourceType.Generic;
        hasResource = false;
    }
}
