using System.Collections;
using UnityEngine;

public class ResourceContainer : MonoBehaviour
{
    public EResourceType resourceType;
    [SerializeField] private int capacity;
    [SerializeField] private int stock;

    public bool hasBeenSeen = false;

    private void Start() {
        hasBeenSeen = true;
    }

    public void Deposit(int units){
        stock += units;
    }

    public Resource Withdraw(int units){
        Resource resource = new Resource(resourceType, 0);

        if (stock >= units){
            stock -= units;
            resource.Add(units);
        }

        return resource;
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

    IEnumerator Walk(){
        yield return new WaitForSeconds(1f);
    }
}