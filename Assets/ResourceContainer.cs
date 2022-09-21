using UnityEngine;

public class ResourceContainer : MonoBehaviour
{
    public EResourceType resourceType;
    [SerializeField] private int capacity;
    [SerializeField] private int stock;

    public ResourceContainer() { }

    public ResourceContainer(EResourceType type)
    {
        resourceType = type;
    }

    private void Start() {
        
    }

    public void Deposit(int units){
        stock += units;
    }

    public int Withdraw(int units){

        if (stock >= units){
            stock -= units;
            return units;
        }

        return 0;
    }

    public int HardWithdraw(int units)
    {    
        if (stock >= units){
            stock -= units;
        }
        else{
            units = stock;
        }

        return units;
    }
}