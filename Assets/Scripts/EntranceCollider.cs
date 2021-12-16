using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceCollider : MonoBehaviour
{

    //Positions where the boss will go
    [SerializeField]
    private Transform posPause;
    [SerializeField]
    private Transform posLaser;
    [SerializeField]
    private Transform posSpawnEnemies;

    [SerializeField]
    private Transform[] positions = new Transform[0];

    [SerializeField]
    private Transform posSpawnBoss;
    [SerializeField]
    private BossController boss;

    [SerializeField]
    private BoxCollider blockEntrance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        BossController bossSpawned = Instantiate(boss, posSpawnBoss);
        bossSpawned.posPause = posPause;
        bossSpawned.posLaser = posLaser;
        bossSpawned.posSpawn = posSpawnEnemies;
        bossSpawned.positions = positions;
        blockEntrance.isTrigger = false;
        Destroy(gameObject);
    }
}
