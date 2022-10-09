using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.WorkerAI
{
    internal class TaskBuildStructure : Node
    {
        private NavMeshAgent agent;
        private Worker worker;

        public TaskBuildStructure(NavMeshAgent agent, Worker worker)
        {
            this.agent = agent;
            this.worker = worker;
        }

        public override NodeState Evaluate()
        {
            Building building = (Building)GetData("building");

            if (building != null)
            {
                agent.SetDestination(building.transform.position);
                Debug.Log($"Destination set: {Vector3.Distance(building.transform.position, agent.transform.position)} remaining");

                if (Vector3.Distance(building.transform.position, worker.transform.position) < 1.5f)
                {
                    building.Build(worker.GiveItem());
                    return NodeState.SUCCESS;
                }
    
            }

            return NodeState.RUNNING;
        }
    }
}
