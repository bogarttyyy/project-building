using BehaviorTree;
using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class TaskWander : Node
{
    private NavMeshAgent agent;
    private float x;
    private float z;
    private Vector3 currentDestination;

    private float waitTime = 1f;
    private float waitCounter = 0;
    private bool waiting;


    public TaskWander(NavMeshAgent navMeshAgent, float xLimit, float zLimit)
    {
        agent = navMeshAgent;
        x = xLimit;
        z = zLimit;
    }

    public override NodeState Evaluate()
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                waiting = false;
            }
        }
        else
        {
            if (agent.isPathStale || currentDestination.AlmostZero())
            {
                currentDestination = WanderToAnotherPoint();
                agent.SetDestination(currentDestination);
            }

            if (Vector3.Distance(currentDestination, agent.transform.position) < 1.5f)
            {
                currentDestination = WanderToAnotherPoint();
                waitCounter = 0;
                waiting = true;
            }
            else
            {
                agent.SetDestination(currentDestination);
                
            }
        }

        state = NodeState.RUNNING;
        return state;
    }

    private Vector3 WanderToAnotherPoint()
    {

        return new Vector3(Random.Range(x * -1, x), 0, Random.Range(z * -1, z));
    }

}
