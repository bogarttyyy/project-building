using BehaviorTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node root = null;
        protected NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        protected void Start()
        {
            root = SetupTree();
        }

        private void Update()
        {
            if (root != null)
            {
                root.Evaluate();
            }
        }

        protected abstract Node SetupTree();
    }
}
