using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBrain : MonoBehaviour
{
    public IAIState currentState;

    #region States
    public ThinkingState thinkingState = new ThinkingState();
    public CollectState collectState = new CollectState();
    public DropState dropState = new DropState();
    public BuildingState buildingState = new BuildingState();

    public MoveState moveState = new MoveState();
    public WanderState wanderState = new WanderState();
    #endregion

    public NavMeshAgent navAgent;
    
    public GameObject destinationObj;
    public Vector3 destination;
    public float destinationDistance;

    public EAiTask currentTask;

    public GameManager gameManager;

    private void OnEnable() {
        currentState = thinkingState;
    }

    private void Start() {
        EventManager.OnNewBuildingPlottedEvent += BuildDesicion;
    }

    private void BuildDesicion(Building building)
    {
        if (currentTask == EAiTask.Wandering)
        {
            currentTask = EAiTask.Building;
            SetDestinationObj(building.gameObject);
        }
    }

    private void Update() {
        currentState = currentState.DoState(this);
        destinationDistance = Vector3.Distance(transform.position, destination);

        switch (currentTask)
        {
            case EAiTask.Building:
                BuildInstructions();
                break;
            case EAiTask.Gathering:
                break;
            case EAiTask.Wandering:
            default:
                break;
        }
    }

    private void BuildInstructions() {
        // Do I have something I'm carrying, is it the right resource?
        // 
        currentState = thinkingState;
    }

    private bool HasArrived() {
        return Vector3.Distance(transform.position, destinationObj.transform.position) < 1.5f;
    }

    private void SetDestinationObj(GameObject gameObject){
        destinationObj = gameObject;
    }

    public void ThinkForAWhile() {
        StartCoroutine(Think());
    }

    IEnumerator Think() {
        yield return new WaitForSeconds(5f); 
    }
}
