using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.FAIL:
                        state = NodeState.FAIL;
                        return state;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            if (anyChildIsRunning)
                state = NodeState.RUNNING;
            else
                state = NodeState.SUCCESS;

            return state;
        }
    }
}
