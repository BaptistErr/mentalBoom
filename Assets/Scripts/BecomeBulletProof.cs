using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeBulletProof : MonoBehaviour
{

    private bool isTurnedBack;
    private float dotProduct;

    // public Transform other;

    // Update is called once per frame
    void Start()
    {
        isTurnedBack = false;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 forward = transform.up;
        Vector3 world = Vector3.up;

        dotProduct = Vector3.Dot(forward, world);

        if (dotProduct >= -0.1 && dotProduct <= 0.1 && isTurnedBack == false)
        {
            isTurnedBack = true;
            gameObject.isStatic = true;
            gameObject.tag = "BulletProof";
        }
    }
}
