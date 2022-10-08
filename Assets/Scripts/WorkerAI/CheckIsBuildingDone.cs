using BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;

namespace Assets.Scripts.WorkerAI
{
    internal class CheckIsBuildingDone : Node
    {

        public override NodeState Evaluate()
        {
            Building building = (Building)GetData("building");

            if (building != null)
            {
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
