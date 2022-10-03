using System;
using UnityEngine;
using UnityEngine.AI;

public class CollectState : IAIState
{
    [SerializeField]
    private ResourceContainer destResourceContainer;

    [SerializeField]
    private NPC npcObj;

    public IAIState DoState(AIBrain aiObject)
    {
        if (aiObject.navAgent == null)
        {
            aiObject.navAgent = aiObject.GetComponent<NavMeshAgent>();
        }

        if (npcObj == null)
        {
            npcObj = aiObject.GetComponent<NPC>();
        }

        if (destResourceContainer == null || !npcObj.hasResource)
        {
            destResourceContainer = GameManager.Instance.GetNearestResourceSpot(aiObject.transform);
            aiObject.destination = destResourceContainer.transform.position;
            aiObject.navAgent.SetDestination(destResourceContainer.transform.position);
        }

        GoCollect(aiObject, destResourceContainer);

        if (npcObj.hasResource){
            Debug.Log("State Changed: Drop");
            return aiObject.dropState;
        }
        else{
            return aiObject.collectState;
        }
    }

    private void GoCollect(AIBrain aiObject, ResourceContainer container)
    {
        if (Vector3.Distance(aiObject.transform.position, container.transform.position) < 1.5f)
        {
            npcObj.AddToContainer(container.Withdraw(1));
        }

        
    }
}