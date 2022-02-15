using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnigmaController : MonoBehaviour
{
    public ColorButton[] buttons;
    public int activatedButtons;
    private bool messageWin = false;
    [SerializeField]
    private GameObject Scissors;

    void Update()
    {
        activatedButtons = 0;
        if (!messageWin)
        {
            foreach (ColorButton button in buttons)
            {
                if (button.IsOverlapped)
                {
                    activatedButtons++;
                }
            }
        }

        if (messageWin == false)
        {
            if (activatedButtons == buttons.Length)
            {
                Debug.Log("Ciseaux, me voila !");
                Scissors.GetComponent<MeshRenderer>().enabled = true;
                Scissors.GetComponent<Rigidbody>().isKinematic = false;

                messageWin = true;
            }
        }
    }
}
