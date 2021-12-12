using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
    public bool IsOverlapped;
    public string BoxName;
    public GameObject Box;

    // Box enter
    private void OnTriggerEnter(Collider other)
    {
        // TODO it repeats too much
        // Box placed?
        if (other.gameObject.name == BoxName)
        {
            Debug.Log(BoxName);
            IsOverlapped = true;
            Box = GameObject.Find(BoxName);
            Box.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}