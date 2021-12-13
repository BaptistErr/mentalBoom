using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnigmaController : MonoBehaviour
{
    public ColorButton[] buttons;
    public int activatedButtons = 0;
    private bool messageWin = false;
    private GameObject Scissors;

    void Update()
    {
        activatedButtons = 0;
        foreach (ColorButton button in buttons)
        {
            if (button.IsOverlapped)
            {
                activatedButtons++;
            }
        }

        if (messageWin == false)
        {
            if (activatedButtons == buttons.Length)
            {

                Scissors = GameObject.Find("Scissors");
                Scissors.GetComponent<MeshRenderer>().enabled = true;
                Scissors.GetComponent<Rigidbody>().isKinematic = false;

                Debug.Log("you won motherfucker");
                messageWin = true;
            }
        }
    }
}