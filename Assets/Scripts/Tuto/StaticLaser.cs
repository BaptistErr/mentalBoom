using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLaser : MonoBehaviour
{
    private GameObject player;
    public GameObject newPosition;

    public bool isTeleported;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(20f, 0f, 0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterController scriptPlayer = player.GetComponent<CharacterController>();
        
        if (!scriptPlayer._dashing)
        {
            player.transform.position = newPosition.transform.position;
            isTeleported = true;
        }
    }
}
