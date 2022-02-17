using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisabler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("TutorialInterface")?.SetActive(false);
    }
}
