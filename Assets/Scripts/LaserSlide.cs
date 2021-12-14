using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSlide : MonoBehaviour
{
    private bool toBack;
    // Start is called before the first frame update
    void Start()
    {
        toBack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (toBack)
        {
            GetComponent<LineRenderer>().SetPosition(1, GetComponent<LineRenderer>().GetPosition(1) - new Vector3(0, 0, .1f));
            if (GetComponent<LineRenderer>().GetPosition(1).z <= -25)
            {
                toBack = false;
            }
        }
        else if (GetComponent<LineRenderer>().GetPosition(1).x <= 66.5)
        {
            GetComponent<LineRenderer>().SetPosition(1, GetComponent<LineRenderer>().GetPosition(1) + new Vector3(0.1f, 0, 0));
        }
        else
        {
            GetComponent<LineRenderer>().SetPosition(1, GetComponent<LineRenderer>().GetPosition(1) + new Vector3(0, 0, .1f));
            if (GetComponent<LineRenderer>().GetPosition(1).z >= 25)
            {
                GetComponentInParent<BossController>().ChangePhase();
                Destroy(gameObject);
            }
        }
    }
}
