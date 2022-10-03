using System;
using UnityEngine;
using UnityEngine.AI;

public class MoveState : IAIState
{
    [SerializeField]
    private Vector3 destTransform;

    public IAIState DoState(AIBrain aiObject)
    {
        if (aiObject.navAgent == null)
        {
            aiObject.navAgent = aiObject.GetComponent<NavMeshAgent>();
        }

        if (destTransform == null)
        {
            destTransform = aiObject.destinationObj.transform.position;
        }

        GoMove(aiObject);

        return ChangeState(aiObject);
    }

    private void GoMove(AIBrain aiObject)
    {
        aiObject.navAgent.SetDestination(destTransform);
    }

    private IAIState ChangeState(AIBrain aiObject){
        return aiObject.moveState;
    }   
}