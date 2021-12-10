using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //Positions where the boss will go
    [SerializeField]
    private Transform posA;
    [SerializeField]
    private Transform posB;
    [SerializeField]
    private Transform posC;
    private Transform[] positions = new Transform[3];
    [SerializeField]
    private Transform posPause;

    private int lastLocation;
    [SerializeField]
    private float speed;
    private bool toC;
    private Vector3 target;
    private bool paused;

    [SerializeField]
    private int health;

    [SerializeField]
    private GameManager manager;

    //Initialize variables
    void Start()
    {
        positions[0] = posA;
        positions[1] = posB;
        positions[2] = posC;
        transform.position = positions[0].position;
        toC = true;
        lastLocation = 0;
        target = positions[1].position;
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        var change = speed * Time.deltaTime;

        if (!paused)
        {
            //Checking direction
            if (toC)
            {
                target = positions[lastLocation + 1].position;
            }
            else
            {
                target = positions[lastLocation - 1].position;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, target, change);

        //Checking if boss is arrived to target
        if (Vector3.Distance(target, transform.position) < 1)
        {
            //Checking direction
            if (toC)
            {
                lastLocation++;
            }
            else
            {
                lastLocation--;
            }

            //Checking if target is terminus
            if (lastLocation == 0)
            {
                toC = true;
            }
            else if (lastLocation == 2)
            {
                toC = false;
            }
        }

        Debug.Log("Time.realtimeSinceStartupAsDouble : " + Time.realtimeSinceStartupAsDouble);
        if (Time.realtimeSinceStartupAsDouble % 5 > 4.9)
        {
            paused = true;
            target = posPause.position;
        }
        
        if (Time.realtimeSinceStartupAsDouble % 10 > 9.9)
        {
            paused = false;
        }
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            manager.Victory();
        }
    }
}