public class Resource
{
    public EResourceType resourceType;
    private int stock;

    public Resource()
    {
        resourceType = EResourceType.Generic;
        stock = 0;
    }

    public Resource(Resource resource)
    {
        resourceType = resource.resourceType;
        stock = resource.stock;
    }

    public Resource(EResourceType type, int count = 0)
    {
        resourceType = type;
        stock = count;
    }

    public void Add(int units){
        stock  += units;
    }

    public int Transfer(int? count = null)
    {
        var toBeTransferred = stock;

        if(count.HasValue)
        {
            if (count.Value <= stock)
            {
                toBeTransferred = count.Value;
                stock -= count.Value;
            }
        }
        else
        {
            stock = 0;
        }

        return toBeTransferred;
    }

    public int Empty()
    {
        var toBeTransferred = stock;
        stock = 0;
        return toBeTransferred;
    }
}