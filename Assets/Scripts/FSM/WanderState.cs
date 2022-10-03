using UnityEngine.AI;
using System;

public class WanderState : IAIState
{
    public IAIState DoState(AIBrain aiObject)
    {
        if (aiObject.navAgent == null)
        {
            aiObject.navAgent = aiObject.GetComponent<NavMeshAgent>();
        }

        GoWander(aiObject);

        return aiObject.wanderState;
    }

    private void GoWander(AIBrain ai)
    {
        throw new NotImplementedException();
    }
}