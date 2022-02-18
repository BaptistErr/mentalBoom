using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform TheHand;
    [SerializeField]
    private GameObject Character;
    public float maxDistance = 2f;
    private bool picking = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // PickUp
            if (!picking && TheHand.childCount.Equals(0))
            {
                float distance = Vector3.Distance(transform.position, Character.transform.position);
                if (distance < maxDistance)
                {
                    GetComponent<BoxCollider>().enabled = false;
                    GetComponent<Rigidbody>().useGravity = false;
                    GetComponent<Light>().enabled = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    transform.position = TheHand.position;
                    transform.parent = GameObject.Find("Hand").transform;
                    picking = true;
                }
            }
            // Throw
            else
            {
                transform.parent = null;
                GetComponent<BoxCollider>().enabled = true;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Light>().enabled = true;
                GetComponent<Rigidbody>().isKinematic = false;
                picking = false;
            }
        }
    }
}