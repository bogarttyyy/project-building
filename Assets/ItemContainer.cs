internal class ItemContainer
{
    public EResourceType resourceType;
    private int stock;

    public ItemContainer(EResourceType type, int? initialStock = null)
    {
        resourceType = type;
        if (initialStock.HasValue)
        {
            stock = initialStock.Value;
        }
        else
        {
            stock = 0;
        }
    }

    public void Deposit(int count){
        stock += count;
    }

    public int Withdraw(int? count){
        int toBeWithdrawn = stock;

        if (count.HasValue)
        {
            if (count.Value <= stock)
            {
                toBeWithdrawn = count.Value;
                stock -= count.Value;
            }
            else
            {            
                stock = 0;
            }
        }
        else
        {
            stock = 0;
        }

        return toBeWithdrawn;
    }
}