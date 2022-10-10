
using NSBStudio.BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.WorkerAI
{
    public class CheckHasUnbuiltStructure : Node
    {
        private Building newBuilding;

        public CheckHasUnbuiltStructure()
        {

        }

        public override NodeState Evaluate()
        {
            Building oldBuilding = (Building)GetData("building");

            if (oldBuilding != null)
            {
                //Debug.Log($"OldBuilding Found: {oldBuilding.transform.position}");
                parent.parent.SetData("currentTask", EAiTask.Building);
                return NodeState.SUCCESS;
            }
                
            newBuilding = CheckForUnbuilt();
            
            if (newBuilding != null)
            {
                Debug.Log($"NewBuilding Found: {newBuilding.transform.position}");
                parent.SetData("building", newBuilding);
                parent.parent.SetData("currentTask", EAiTask.Building);
                newBuilding = null;
                return NodeState.SUCCESS;
            }

            return NodeState.FAILURE;
        }
        private Building CheckForUnbuilt()
        {
            return GameManager.Instance?.GetStructuresToBuild()?.FirstOrDefault();
        }

    }
}
