using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Camera cam;

    [SerializeField]
    private Transform movePositionTransform;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();    
    }

    private void Update() {
        // navMeshAgent.destination = movePositionTransform.position;

        if (Input.GetMouseButtonDown(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (!movePositionTransform.gameObject.activeSelf)
                {
                    movePositionTransform.gameObject.SetActive(true);
                }
                
                movePositionTransform.position = hit.point;
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }
}
