using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerAttack : MonoBehaviour
{
    public int EnemyLayer = 11;
    
    private HashSet<GameObject> _enemies;
    public IEnumerable<GameObject> Enemies => _enemies;

    public BoxCollider Collider { get; private set; }

    private Vector3 _localPos;
    private Quaternion _localRot;
    private Transform _parent;
    
    private void Awake()
    {
        _enemies = new HashSet<GameObject>();
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

        CleanUp();
    }

    private void CleanUp()
    {
        foreach (GameObject go in _enemies.ToList())
        {
            if (go == null)
            {
                _enemies.Remove(go);
            }
        }
        Debug.Log(_enemies.Count);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        Debug.Log("enter " + go);
        if (go.layer == EnemyLayer)
        {
            _enemies.Add(go);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject;
        Debug.Log("exit " + go);
        if (go.layer == EnemyLayer)
        {
            _enemies.Remove(go);
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject;
        Debug.Log("exit " + go);
        if (go.layer == EnemyLayer)
        {
            _enemies.Remove(go);
        }
    }*/
}
