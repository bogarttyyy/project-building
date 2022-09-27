using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBrain : MonoBehaviour
{
    public IAIState currentState;

    public ThinkingState thinkingState = new ThinkingState();
    public CollectState collectState = new CollectState();
    public DropState dropState = new DropState();

    public MoveState moveState = new MoveState();
    public WanderState wanderState = new WanderState();

    public NavMeshAgent navAgent;
    public Vector3 destination;
    public float destinationDistance;

    public GameManager gameManager;

    private void OnEnable() {
        currentState = thinkingState;
    }

    private void Update() {
        currentState = currentState.DoState(this);
        destinationDistance = Vector3.Distance(transform.position, destination);
    }

    public void ThinkForAWhile() {
        StartCoroutine(Think());
    }

    IEnumerator Think() {
        yield return new WaitForSeconds(5f); 
    }
}
