using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour, IEnemy
{

    //Positions where the boss will go
    public Transform posPause;
    public Transform posLaser;
    public Transform posSpawn;

    public Transform[] positions = new Transform[0];

    private int lastLocation;
    [SerializeField]
    private float speed;
    private bool toC;
    private Vector3 target;
    public bool paused;
    private int pausesCounter;

    public bool isLasering;

    public bool isSpawning;

    [SerializeField]
    private int health;

    private GameManager manager;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject laser;

    [SerializeField]
    private GameObject enemy;

    private Coroutine shoot;

    private int pattern;
    private int lastPattern;

    private bool phaseChanged;

    private int enemiesCounter;

    //Initialize variables
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        toC = true;
        lastLocation = 0;
        target = positions[2].position;
        paused = false;
        pattern = 0;
        lastPattern = pattern;
        phaseChanged = false;
        pausesCounter = 0;
        isLasering = false;
        isSpawning = false;

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

    IEnumerator Pause()
    {
        paused = true;
        target = posPause.position;
        yield return new WaitForSeconds(5);
        paused = false;
        pausesCounter++;
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesCounter; i++)
        {
            Instantiate(enemy, posSpawn);
            yield return new WaitForSeconds(1);
        }
        isSpawning = false;
        ChoosePattern();
    }

    // Update is called once per frame
    void Update()
    {
        if (pattern == 0 && !paused)
        {
            Movement();
            if (health <= 100 && !phaseChanged)
            {
                StopCoroutine(shoot);
                ChoosePattern();
                phaseChanged = true;
                paused = false;
            }
            if (pausesCounter == 2 && phaseChanged)
            {
                pausesCounter = 0;
                paused = false;
                StopCoroutine(shoot);
                ChoosePattern();
            }
        }
        else if (Vector3.Distance(target, transform.position) < 1 && !isLasering && pattern == 1)
        {
            pausesCounter = 0;
            Instantiate(laser, transform.position, transform.rotation, transform);
            isLasering = true;
        }
        else if (Vector3.Distance(target, transform.position) < 1 && !isSpawning && pattern == 2)
        {
            StartCoroutine(SpawnEnemies());
            isSpawning = true;
        }
        var step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    public void ChoosePattern()
    {
        while (lastPattern == pattern)
        {
            pattern = Random.Range(0, 3);
        }
        lastPattern = pattern;
        Debug.Log("pattern : " + pattern);

        if (pattern == 0)
        {
            shoot = StartCoroutine(Shoot());
        }
        else if (pattern == 1)
        {
            target = posLaser.position;
        }
        else if (pattern == 2)
        {
            target = posLaser.position;
            enemiesCounter = Random.Range(2, 7);
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

    private void Movement()
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
                if (Random.Range(0, 2) == 0)
                {
                    StartCoroutine(Pause());
                }
            }
        }
    }
}
