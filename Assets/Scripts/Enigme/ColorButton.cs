using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
    public bool IsOverlapped;
    public GameObject Box;

    // Box enter
    private void OnTriggerEnter(Collider other)
    {
        // Box placed?
        if (other.gameObject == Box)
        {
            IsOverlapped = true;
            Box.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}