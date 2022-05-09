using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour, IEnemy
{
    private bool hasBegun;
    private bool toC;
    private bool phaseChanged;
    private int lastLocation;
    private int pausesCounter;
    private int pattern;
    private int lastPattern;
    private int enemiesCounter;
    private float health;
    private Coroutine shoot;
    private Coroutine grindHealth;
    private GameManager manager;
    private Vector3 target;

    [SerializeField]
    private int maxEnemies;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private IAChasing_Controller enemy;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private GameObject bossUI;

    public bool paused;
    public bool isLasering;
    public bool isSpawning;
    public int totalEnemiesAlive;
    public Transform posPause;
    public Transform posLaser;
    public Transform posSpawn;
    public Transform[] positions = new Transform[0];

    public static BossController Instance { get; private set; }

    //Initialize variables
    void Start()
    {
        Instance = this;

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
        hasBegun = false;
        health = 0;
        totalEnemiesAlive = 0;
    }

    public void Begin()
    {
        bossUI.SetActive(true);

        grindHealth = StartCoroutine(GrindHealth());

        shoot = StartCoroutine(Shoot());

        hasBegun = true;
    }

    IEnumerator GrindHealth()
    {
        while (health != maxHealth)
        {
            health+=0.5f;
            healthBar.fillAmount = health / maxHealth;
            yield return new WaitForSeconds(.01f);
        }
        StopCoroutine(grindHealth);
    }

    IEnumerator Shoot()
    {
        while (!GameManager.HasGameEnded && health > 0)
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
            if (totalEnemiesAlive < maxEnemies)
            {
                IAChasing_Controller enemySpawned = Instantiate(enemy, posSpawn);
                enemySpawned.boss = this;
                totalEnemiesAlive++;
            }
            yield return new WaitForSeconds(1);
        }
        isSpawning = false;
        ChoosePattern();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBegun)
        {
            if (pattern == 0 && !paused)
            {
                Movement();
                if (health < 100 && !phaseChanged && manager.enigmaFinished)
                {
                    StopCoroutine(shoot);
                    ChoosePattern();
                    phaseChanged = true;
                    paused = false;
                }
                if (pausesCounter == 2 && phaseChanged && manager.enigmaFinished)
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
    }
    
    private void LateUpdate()
    {
        Minimap.Instance.BossPosition = transform.position;
    }

    public void ChoosePattern()
    {
        while (lastPattern == pattern)
        {
            pattern = Random.Range(0, 3);
        }
        lastPattern = pattern;

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
        healthBar.fillAmount = health / maxHealth;
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
