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

    private Vector3 _localPos;
    private Quaternion _localRot;
    private Transform _parent;
    
    private void Awake()
    {
        _enemies = new List<GameObject>(capacity: 8);
        Collider = GetComponent<BoxCollider>();

        var transform1 = transform;
        _localPos = transform1.localPosition;
        _localRot = transform1.localRotation;
        _parent = transform1.parent;
        transform1.parent = null;
    }

    private void LateUpdate()
    {
        transform.parent = _parent;
        transform.localRotation = _localRot;
        transform.localPosition = _localPos;
        transform.parent = null;
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
