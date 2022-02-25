using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Dummy : MonoBehaviour, IEnemy
{
    private float dummyHealth;

    [SerializeField]
    private float dummyMaxHealth;
    [SerializeField]
    private Canvas healthBarCanvas;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private float _damage = 10.0F;

    public bool isDestroy;

    private void Start()
    {
        dummyHealth = dummyMaxHealth;
        isDestroy = false;
    }

    /// <summary> Amount of damages dealt by each projectile </summary>
    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }


    public void GetDamage(int damage)
    {
        dummyHealth -= damage;
        healthBar.fillAmount = dummyHealth / dummyMaxHealth;
        if (dummyHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        isDestroy = false;
    }
}
