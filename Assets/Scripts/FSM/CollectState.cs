using System;
using UnityEngine;
using UnityEngine.AI;

public class CollectState : IAIState
{
    [SerializeField]
    private ResourceContainer destContainer;

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

        if (destContainer == null || !npcObj.hasResource)
        {
            destContainer = GameManager.Instance.GetNearestResourceSpot(aiObject.transform);
            aiObject.destination = destContainer.transform.position;
            aiObject.navAgent.SetDestination(destContainer.transform.position);
        }

        GoCollect(aiObject, destContainer);

        return ChangeState(aiObject);
    }

    private IAIState ChangeState(AIBrain aiObject)
    {
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
            npcObj.AddToInventory(container.Withdraw(1));
        }
    }
}