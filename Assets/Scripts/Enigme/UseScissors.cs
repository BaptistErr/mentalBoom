using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseScissors : MonoBehaviour
{
    private GameObject Plateform;
    [SerializeField]
    private GameObject Scissors;
    private GameObject WallToRemove;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Scissors)
        {
            Plateform = GameObject.Find("Plateform");
            Plateform.GetComponent<Rigidbody>().isKinematic = false;

            WallToRemove = GameObject.Find("WallToRemove");
            WallToRemove.GetComponent<MeshRenderer>().enabled = false;
            WallToRemove.GetComponent<BoxCollider>().enabled = false;

            Scissors.GetComponent<BoxCollider>().enabled = false;
            Scissors.GetComponent<MeshRenderer>().enabled = false;

            StartCoroutine(WaitBeforeStuck());
        }
    }

    IEnumerator WaitBeforeStuck()
    {
        // yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
        // stuck the bridge
        Plateform.GetComponent<Rigidbody>().isKinematic = true;
    }

}
