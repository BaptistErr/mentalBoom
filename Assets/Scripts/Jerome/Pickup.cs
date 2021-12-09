using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform TheHand;
    private GameObject Character;
    public float MaxDistance = 1f;

    //Pick
    void OnMouseDown()
    {
        Character = GameObject.Find("Character");
        float distance = Vector3.Distance(this.transform.position, Character.transform.position);
        if(distance < MaxDistance)
        {
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            this.transform.position = TheHand.position;
            this.transform.parent = GameObject.Find("Hand").transform;
        }
    }

    //Throw
    void OnMouseUp()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<SphereCollider>().enabled = true;
    }
}