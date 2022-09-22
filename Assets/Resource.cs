public class Resource
{
    public EResourceType resourceType;
    private int stock;

    public Resource() { }

    public Resource(EResourceType type, int count)
    {
        resourceType = type;
        stock = count;
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