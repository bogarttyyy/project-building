using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviorTree
{
    public class WorkerBT : Tree
    {
        protected override Node SetupTree()
        {
            Node root = new TaskWander(agent, 5, 5);

            return root;
        }
    }
}
