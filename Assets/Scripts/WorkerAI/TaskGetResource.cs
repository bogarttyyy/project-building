using NSBStudio.BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class TaskGetResource : Node
{
    private Worker worker;
    private NavMeshAgent agent;
    private Home home;

    public TaskGetResource(Worker worker, NavMeshAgent agent, Home home)
    {
        this.worker = worker;
        this.agent = agent;
        this.home = home;
    }

    public override NodeState Evaluate()
    {
        if (worker.hasResource)
        {
            return NodeState.SUCCESS;
        }

        Building building = (Building)GetData("building");

        if (building != null)
        {
            agent.SetDestination(home.transform.position);

            if (Vector3.Distance(agent.transform.position, home.transform.position) < 1.5f)
            {
                worker.AddToInventory(home.GetResource(building.resourceMaterial, 1));
                return NodeState.SUCCESS;
            }
        }

        return NodeState.RUNNING;
    }
}
