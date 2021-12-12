using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerAttack : MonoBehaviour
{
    public int EnemyLayer = 11;
    
    private List<GameObject> _enemies;
    public IEnumerable<GameObject> Enemies => _enemies;

    public BoxCollider Collider { get; private set; } 
    
    private void Awake()
    {
        _enemies = new List<GameObject>(capacity: 8);
        Collider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.layer == EnemyLayer)
        {
            _enemies.Add(go);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.layer == EnemyLayer)
        {
            _enemies.Remove(go);
        }
    }
}
