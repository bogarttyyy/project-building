using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private int units;
    public EResourceType resourceType;

    public Resource() { }

    public Resource(EResourceType type)
    {
        resourceType = type;
    }

    public Resource(EResourceType type, int count)
    {
        resourceType = type;
        units = count;
    }

    private void Start() {

    }

    public int Transfer(int? units = null)
    {
        // if (units.HasValue)
        // {
            
        // }
        // else
        // {
        //     return 
        // }

        return 0;
    }

    public int Empty()
    {
        var toBeTransferred = units;
        units = 0;
        return toBeTransferred;
    }
}