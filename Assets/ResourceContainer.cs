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

    IEnumerator Walk(){
        yield return new WaitForSeconds(1f);
    }
}