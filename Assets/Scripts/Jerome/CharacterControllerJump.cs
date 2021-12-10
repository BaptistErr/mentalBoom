using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(
    typeof(CapsuleCollider), typeof(Rigidbody))
]
public class CharacterControllerJump : MonoBehaviour
{
    public float MoveSpeed = 5.0F;
    public bool CharacterOnFloor = true;

    private Rigidbody rb;

    public Vector3 ForwardAxis = new Vector3(-1.0F, 0.0F, 1.0F).normalized;
    public Vector3 RightAxis = new Vector3(1.0F, 0.0F, 1.0F).normalized;
    public Vector3 BackwardAxis => -ForwardAxis;
    public Vector3 LeftAxis => -RightAxis;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Move
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 dir = ForwardAxis * vertical + RightAxis * horizontal;
        Vector3 translation = dir * MoveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + translation);
    }

    //Jump
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && CharacterOnFloor)
        {
            rb.AddForce(new Vector3(0, 50, 0), ForceMode.Impulse);
            CharacterOnFloor = false;
        }
    }

    //Jump only on "Jumpable" tag objects
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Jumpable")
        {
            CharacterOnFloor = true;
        }
    }
}