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
        BasicColor = new Color(123f/255f, 99f/255f, 83f/255f);      // Pink
        ActivateColor = new Color(224f/255f, 193f/255f, 172f/255f);  // Purple

        isInTrigger = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && isInTrigger == true && NbHeal > 0 && CharacterController.Instance.Health != CharacterController.Instance.MaxHealth)
        {
            Debug.Log("HEAL");
            NbHeal--;
            CharacterController.Instance.Heal(CharacterController.Instance.MaxHealth);
            SetColor(ActivateColor);
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
            SetColor(BasicColor);
            isInTrigger = false;
        }
    }

    public void SetColor(Color value)
    {
        myMaterial.color = value;
    }
}
