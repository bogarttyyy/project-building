﻿using NSBStudio.BehaviorTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.WorkerAI
{
    public class CheckIsCorrectResource : Node
    {
        private Worker npc;

        public CheckIsCorrectResource(Worker npc)
        {
            this.npc = npc;
        }

        public override NodeState Evaluate()
        {
            Building building = (Building)GetData("building");

            if (npc.hasResource)
            {
                if (building.resourceMaterial == npc.ItemHeld().resourceType)
                {
                    return NodeState.FAILURE;
                }
                npc.DropItem();
            }

            return NodeState.RUNNING;
        }
    }
}
