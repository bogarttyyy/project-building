using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorTree
{
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.FAIL:
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }
            
            state = NodeState.FAIL;

            return state;
        }
    }
}
