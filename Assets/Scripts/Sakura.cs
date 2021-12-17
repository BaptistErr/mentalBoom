using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura : MonoBehaviour
{

    [SerializeField]
    private GameObject character;
    public int NbHeal = 3;

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == character)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && NbHeal > 0)
            {
                Debug.Log("HEAL");
                NbHeal--;
            }
        }
    }
}
