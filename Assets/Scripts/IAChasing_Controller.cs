using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class IAChasing_Controller : MonoBehaviour, IEnemy
{

    private NavMeshAgent agent = null;
    private CharacterController target;
    private Vector3 aim;
    private GameObject IAChasing;
    private float stoppingDistance;
    private float IAHealth;
    private Coroutine coroutine;

    [SerializeField]
    private float IAMaxHealth;
    [SerializeField]
    private Canvas healthBarCanvas;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private float _damage = 10.0F;

    /// <summary> Amount of damages dealt by each projectile </summary>
    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }
    public BossController boss;


    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
        target = FindObjectOfType<CharacterController>();
        IAChasing = GameObject.Find("AIChasing(Clone)");
        stoppingDistance = IAChasing.GetComponent<NavMeshAgent>().stoppingDistance;

        IAHealth = IAMaxHealth;
    }

    // Update
    private void Update()
    {
        MoveToTarget();
        healthBarCanvas.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
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
        IAHealth -= damage;
        healthBar.fillAmount = IAHealth / IAMaxHealth;
        if (IAHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        boss.totalEnemiesAlive--;
        // Debug.Log("I'm dead");
        Destroy(gameObject);
    }
}
