using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnigmaController : MonoBehaviour
{
    public ColorButton[] buttons;
    public int activatedButtons = 0;
    private bool messageWin = false;
    [SerializeField]
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
                Scissors.GetComponent<MeshRenderer>().enabled = true;
                Scissors.GetComponent<Rigidbody>().isKinematic = false;
                Scissors.GetComponent<MeshCollider>().enabled = true;

                messageWin = true;
            }
        }
    }
}
