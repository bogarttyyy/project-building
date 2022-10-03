using UnityEngine;
using UnityEngine.AI;
using System;

public class BuildingState : IAIState
{
    [SerializeField]
    private Building building;

    public IAIState DoState(AIBrain aiObject)
    {
        if (aiObject.navAgent == null)
        {
            aiObject.navAgent = aiObject.GetComponent<NavMeshAgent>();
        }

        if (building == null)
        {
            building = GameManager.Instance.GetStructureToBuild(aiObject.transform);
        }

        GoBuild(aiObject);

        if (building.IsBuilt()){
            return aiObject.thinkingState;
        }
        else {
            return aiObject.buildingState;
        }
    }

    private void GoBuild(AIBrain ai)
    {
        throw new NotImplementedException();
    }
}