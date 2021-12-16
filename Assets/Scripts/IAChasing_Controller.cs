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
    [SerializeField]
    private int IA_health;
    private Coroutine coroutine;
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
        IA_health = 60;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            coroutine = StartCoroutine(WaitBeforeHit());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            StopCoroutine(coroutine);
        }
    }

    // Attack the player
    private void Attack()
    {
        target.GetDamage(Damage);
        Debug.Log(target.Health);
    }

    // Cooldown before each hit
    IEnumerator WaitBeforeHit()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Attack();
        }
    }

    public void GetDamage(int damage)
    {
        if (IA_health <= 0)
        {
            Die();
        }
        else
        {
            IA_health -= damage;
        }
    }

    public void Die()
    {
        Debug.Log("I'm dead");
        Destroy(gameObject);
    }
}
