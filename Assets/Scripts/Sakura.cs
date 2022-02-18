using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura : MonoBehaviour
{

    [SerializeField] private GameObject character;
    [SerializeField] private Material myMaterial;

    // Set up colors of the Hub
    private Color BasicColor;
    private Color ActivateColor;

    bool isInTrigger;
    private int NbHeal = 3;

    private void Start()
    {
        isInTrigger = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && isInTrigger == true && NbHeal > 0 && CharacterController.Instance.Health != CharacterController.Instance.MaxHealth)
        {
            NbHeal--;
            CharacterController.Instance.Heal(CharacterController.Instance.MaxHealth);
        }
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            isInTrigger = true;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            isInTrigger = false;
        }
    }
}
