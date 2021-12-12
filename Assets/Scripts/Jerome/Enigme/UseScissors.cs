using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseScissors : MonoBehaviour
{
    private GameObject Plateform;
    private GameObject Scissors;
    private GameObject WallToRemove;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Scissors")
        {
            Plateform = GameObject.Find("Plateform");
            Plateform.GetComponent<Rigidbody>().isKinematic = false;

            WallToRemove = GameObject.Find("WallToRemove");
            WallToRemove.GetComponent<MeshRenderer>().enabled = false;
            WallToRemove.GetComponent<BoxCollider>().enabled = false;

            Scissors = GameObject.Find("Scissors");
            Scissors.GetComponent<MeshRenderer>().enabled = false;
            Scissors.GetComponent<SphereCollider>().enabled = false;
            Debug.Log("You used SCISSORS");

            StartCoroutine(WaitBeforeStuck());
        }
    }

    IEnumerator WaitBeforeStuck()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);
    }

}
