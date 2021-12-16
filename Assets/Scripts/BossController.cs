using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //Positions where the boss will go
    [SerializeField]
    private Transform[] positions = new Transform[0];
    [SerializeField]
    private Transform posPause;
    [SerializeField]
    private Transform posLaser;

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

    [SerializeField]
    private GameObject laser;

    private int actualPhase;

    private Coroutine shoot;

    private bool isLasering;

    //Initialize variables
    void Start()
    {
        transform.position = positions[0].position;
        toC = true;
        lastLocation = 0;
        target = positions[1].position;
        paused = false;
        actualPhase = 0;
        isLasering = false;

        shoot = StartCoroutine(Shoot());
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
        if (actualPhase == 0)
        {
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
                ChangePhase();
            }
        }
        else
        {
            target = posLaser.position;
            if (Vector3.Distance(target, transform.position) < 1 && !isLasering)
            {
                Instantiate(laser, transform.position, transform.rotation, transform);
                isLasering = true;
            }
        }
        var step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
        manager.GameEnded(true);
    }

    public void ChangePhase()
    {
        if (actualPhase == 0)
        {
            actualPhase++;
            StopCoroutine(shoot);
        }
        else
        {
            actualPhase--;
            shoot = StartCoroutine(Shoot());
        }
    }
}
