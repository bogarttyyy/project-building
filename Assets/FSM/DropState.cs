using System;
using UnityEngine;
using UnityEngine.AI;

public class DropState : IAIState
{
    [SerializeField]
    private Home homebase;

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

        if (homebase == null || npcObj.hasResource)
        {
            homebase = GameManager.Instance.GetHomebase(aiObject.transform);
            aiObject.destination = homebase.transform.position;
            aiObject.navAgent.SetDestination(homebase.transform.position);
        }

        GoDrop(aiObject, homebase);

        if (!npcObj.hasResource){
            Debug.Log("State Changed: Thinking");
            return aiObject.thinkingState;
        }
        else
            return aiObject.dropState;
    }

    private void GoDrop(AIBrain aiObject, Home homebase)
    {
        if (Vector3.Distance(aiObject.transform.position, homebase.transform.position) < 1.5f)
        {
            homebase.DepositResource(npcObj.EmptyContainer());
            npcObj.hasResource = false;
        }
    }
}