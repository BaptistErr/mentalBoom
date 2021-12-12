using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float force;

    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
        target = FindObjectOfType<CharacterController>().transform.position;
        transform.LookAt(target);
        transform.rotation *= Quaternion.Euler(90, 0, 0);
        GetComponent<Rigidbody>().AddForce(transform.up * force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
