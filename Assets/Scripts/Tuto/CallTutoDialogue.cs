using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTutoDialogue : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;

    // Start is called before the first frame update
    void Start()
    {
        dialogueTrigger = GameObject.Find("TutoChat_Dummy").GetComponent<DialogueTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        dialogueTrigger.TriggerDialogue();
        Destroy(this);
    }
}