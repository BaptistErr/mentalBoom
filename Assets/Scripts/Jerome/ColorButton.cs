using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{

    public bool RedIsOverlap = false;
    public bool GreenIsOverlap = false;
    public bool BlueIsOverlap = false;
    public bool YellowIsOverlap = false;


    // Box enter
    private void OnTriggerEnter(Collider other)
    {
        //Red one is placed
        if(this.gameObject.name == "RedSlab")
        {
            if (other.gameObject.name == "RedBox")
            {
                Debug.Log("ROUGE");
                RedIsOverlap = true;
            }
        }
        //Green one is placed
        if (this.gameObject.name == "GreenSlab")
        {
            if (other.gameObject.name == "GreenBox")
            {
                Debug.Log("VERT");
                GreenIsOverlap = true;
            }
        }
        //Blue one is placed
        if (this.gameObject.name == "BlueSlab")
        {
            if (other.gameObject.name == "BlueBox")
            {
                Debug.Log("BLEU");
                BlueIsOverlap = true;
            }
        }
        //Yellow one is placed
        if (this.gameObject.name == "YellowSlab")
        {
            if (other.gameObject.name == "YellowBox")
            {
                Debug.Log("JAUNE");
                YellowIsOverlap = true;
            }
        }
    }


    // Box exit
    private void OnTriggerExit(Collider other)
    {
        //Red one is removed
        if (this.gameObject.name == "RedSlab")
        {
            if (other.gameObject.name == "RedBox")
            {
                RedIsOverlap = false;
            }
        }
        //Green one is removed
        if (this.gameObject.name == "GreenSlab")
        {
            if (other.gameObject.name == "GreenBox")
            {
                GreenIsOverlap = false;
            }
        }
        //Blue one is removed
        if (this.gameObject.name == "BlueSlab")
        {
            if (other.gameObject.name == "BlueBox")
            {
                BlueIsOverlap = false;
            }
        }
        //Yellow one is removed
        if (this.gameObject.name == "YellowSlab")
        {
            if (other.gameObject.name == "YellowBox")
            {
                YellowIsOverlap = false;
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if(RedIsOverlap == true && GreenIsOverlap == true && BlueIsOverlap == true && YellowIsOverlap == true)
        {
            Debug.Log("Congrats ! You win mother fucker !");
        }
    }
}
