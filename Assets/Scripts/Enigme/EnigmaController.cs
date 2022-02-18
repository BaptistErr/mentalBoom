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

    private GameManager manager;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

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
                Scissors.GetComponent<MeshRenderer>().enabled = true;
                Scissors.GetComponent<Rigidbody>().isKinematic = false;

                messageWin = true;
                manager.enigmaFinished = messageWin;
            }
        }
    }
}
