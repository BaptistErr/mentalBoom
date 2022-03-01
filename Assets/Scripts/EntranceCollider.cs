using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceCollider : MonoBehaviour
{
    [SerializeField]
    private BossController boss;

    [SerializeField]
    private GameObject blockEntrance;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            boss.Begin();
            blockEntrance.SetActive(true);
            Destroy(gameObject);
        }
    }
}
