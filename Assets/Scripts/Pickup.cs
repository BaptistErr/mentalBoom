using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform TheHand;
    private GameObject Character;
    public float maxDistance;
    private bool picking;

    private void Start()
    {
        picking = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // PickUp
            if (!picking && TheHand.childCount.Equals(0))
            {
                Character = GameObject.Find("Character");
                float distance = Vector3.Distance(transform.position, Character.transform.position);
                if (distance < maxDistance)
                {
                    GetComponent<MeshCollider>().enabled = false;
                    GetComponent<Rigidbody>().useGravity = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    transform.position = TheHand.position;
                    transform.parent = GameObject.Find("Hand").transform;
                    picking = true;
                    Debug.Log("get");
                }
            }
            // Throw
            else
            {
                transform.parent = null;
                GetComponent<MeshCollider>().enabled = true;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;
                picking = false;
                Debug.Log("throw");
            }
        }
    }
}