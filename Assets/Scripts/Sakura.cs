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
        if (other.GetComponent<CharacterController>())
        {
            if (Input.GetKeyDown(KeyCode.A) && NbHeal > 0 && CharacterController.Instance.Health != CharacterController.Instance.MaxHealth)
            {
                Debug.Log("HEAL");
                NbHeal--;
                CharacterController.Instance.Heal(CharacterController.Instance.MaxHealth);
            }
        }
    }
}
