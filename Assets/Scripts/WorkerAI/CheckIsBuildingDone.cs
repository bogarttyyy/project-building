using NSBStudio.BehaviorTree;
using UnityEngine;

namespace Assets.Scripts.WorkerAI
{
    internal class CheckIsBuildingDone : Node
    {

        public override NodeState Evaluate()
        {
            Building building = (Building)GetData("building");

            if (building != null)
            {
                Debug.Log($"Check Built: {building.IsBuilt()}");
                if (building.IsBuilt())
                {
                    ClearData("building");
                    return NodeState.SUCCESS;
                }
            }

            return NodeState.RUNNING;
        }
    }
}
