using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    internal class TaskDropResource : Node
    {
        private GameObject destination;
        private Worker worker;
        private NavMeshAgent agent;

        public TaskDropResource(GameObject destination, Worker worker, NavMeshAgent agent)
        {
            this.destination = destination;
            this.worker = worker;
            this.agent = agent;
        }

        public override NodeState Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}