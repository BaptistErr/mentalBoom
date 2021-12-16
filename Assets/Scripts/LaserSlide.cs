using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSlide : MonoBehaviour
{
    private bool toBack;

    private float distanceTo0;
    private float distanceTo1;
    private float distDiff;
    private float lengthLaser;

    private CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        toBack = true;
        character = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toBack)
        {
            GetComponent<LineRenderer>().SetPosition(1, GetComponent<LineRenderer>().GetPosition(1) - new Vector3(0, 0, .05f));
            if (GetComponent<LineRenderer>().GetPosition(1).z <= -25)
            {
                toBack = false;
            }
        }
        else if (GetComponent<LineRenderer>().GetPosition(1).x <= 66.5)
        {
            GetComponent<LineRenderer>().SetPosition(1, GetComponent<LineRenderer>().GetPosition(1) + new Vector3(0.05f, 0, 0));
        }
        else
        {
            GetComponent<LineRenderer>().SetPosition(1, GetComponent<LineRenderer>().GetPosition(1) + new Vector3(0, 0, .05f));
            if (GetComponent<LineRenderer>().GetPosition(1).z >= 25)
            {
                GetComponentInParent<BossController>().ChoosePattern();
                GetComponentInParent<BossController>().isLasering = false;
                Destroy(gameObject);
            }
        }

        distanceTo0 = Vector3.Distance(character.transform.position, GetComponent<LineRenderer>().GetPosition(0));
        distanceTo1 = Vector3.Distance(character.transform.position, GetComponent<LineRenderer>().GetPosition(1));
        lengthLaser = Vector3.Distance(GetComponent<LineRenderer>().GetPosition(0), GetComponent<LineRenderer>().GetPosition(1));

        distDiff = distanceTo0 + distanceTo1 - lengthLaser;

        if (distDiff > -0.1 && distDiff < 0.1)
        {
            character.GetDamage(40);
        }
    }
}
