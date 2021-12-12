using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAChasing_Controller : MonoBehaviour
{

    private NavMeshAgent agent = null;
    //[SerializeField] private Transform target;
    private Vector3 target;

    private GameObject IAChasing;
    private float stoppingDistance;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
        IAChasing = GameObject.Find("AIChasing");
        stoppingDistance = IAChasing.GetComponent<NavMeshAgent>().stoppingDistance;
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        target = FindObjectOfType<CharacterController>().transform.position;
        agent.SetDestination(target);

        float distanceToTarget = Vector3.Distance(transform.position, target);

        
        if (distanceToTarget <= stoppingDistance)
        {
            RotateToTarget();
        }
    }

    private void RotateToTarget()
    {
        transform.LookAt(target);
    }

    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
    }
}
