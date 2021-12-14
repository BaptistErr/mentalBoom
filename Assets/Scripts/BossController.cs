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

    [SerializeField]
    private BulletBehaviour bullet;

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

        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (!manager.gameEnded && health > 0)
        {
            if (!paused)
            {
                Instantiate(bullet, transform.position + new Vector3(-5, 0, -5), transform.rotation);
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;

        if (!paused && health > 0)
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

        transform.position = Vector3.MoveTowards(transform.position, target, step);

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

        if (Time.realtimeSinceStartupAsDouble % 5 > 4.9 && ((lastLocation == 1 && toC) || lastLocation == 2) && !paused)
        {
            paused = true;
            target = posPause.position;
        }
        
        if (Time.realtimeSinceStartupAsDouble % 10 > 9.9 && paused)
        {
            paused = false;
        }
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            transform.rotation = Quaternion.Euler(90, 0, 0);
            manager.GameEnded(true);
        }
    }
}
