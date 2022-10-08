using Assets.Scripts.WorkerAI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    public class WorkerBT : Tree
    {
        [SerializeField] private Home home;

        [SerializeField] private EAiTask currentTask;

        [SerializeField] private Worker worker;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Building building;

        private Transform destination;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            worker = GetComponent<Worker>();
        }

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>()
            {
                new Sequence(new List<Node>()
                {
                    new CheckHasUnbuiltStructure(),
                    new Sequence(new List<Node>()
                    {
                        new TaskGetResource(worker, agent, home),
                        new CheckIsCorrectResource(worker)
                    }),
                    new TaskBuildStructure(agent, worker),
                    new CheckIsBuildingDone()
                }),
                //new TaskWander(agent, 5, 5)
            });

            return root;
        }
    }
}
