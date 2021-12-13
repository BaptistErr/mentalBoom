using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAChasing_Controller : MonoBehaviour
{

    private NavMeshAgent agent = null;
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

    // Update
    private void Update()
    {
        MoveToTarget();
    }

    // Move to player
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

    // Rotate toward player
    private void RotateToTarget()
    {
        transform.LookAt(target);
    }

    // GetReferences
    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Detecting player
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Character")
        {
            Attack();
        }
    }

    // Attack the player
    private void Attack()
    {
        Debug.Log("COUCOU");
    }
}
