using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAChasing_Controller : MonoBehaviour
{

    private NavMeshAgent agent = null;
    private CharacterController target;
    private Vector3 aim;
    private GameObject IAChasing;
    private float stoppingDistance;
    private bool Cooldown = true;

    [SerializeField] private float _damage = 10.0F;
    /// <summary> Amount of damages dealt by each projectile </summary>
    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }


    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
        target = FindObjectOfType<CharacterController>();
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
        aim = FindObjectOfType<CharacterController>().transform.position;
        agent.SetDestination(aim);

        float distanceToTarget = Vector3.Distance(transform.position, aim);

        
        if (distanceToTarget <= stoppingDistance)
        {
            RotateToTarget();
        }
    }

    // Rotate toward player
    private void RotateToTarget()
    {
        transform.LookAt(aim);
    }

    // GetReferences
    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Detecting player
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterController>() && Cooldown == true)
        {
            Attack();
            Cooldown = false;
            StartCoroutine(WaitBeforeHit());
        }
    }

    // TODO ca fait pas les degat marqué au dessus soit 10
    // Attack the player
    private void Attack()
    {
        target.GetDamage(Damage);
        Debug.Log(target.Health);
    }

    // Cooldown before each hit
    IEnumerator WaitBeforeHit()
    {
        // yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
        Cooldown = true;
    }
}
