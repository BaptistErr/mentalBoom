using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(
    typeof(CapsuleCollider), typeof(Rigidbody))
]
public class CharacterController : MonoBehaviour
{
    public float MoveSpeed = 5.0F;
    
    private Rigidbody _rb;

    public Vector3 ForwardAxis = new Vector3(-1.0F, 0.0F, 1.0F).normalized;
    public Vector3 RightAxis = new Vector3(1.0F, 0.0F, 1.0F).normalized;
    public Vector3 BackwardAxis => -ForwardAxis;
    public Vector3 LeftAxis => -RightAxis;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 dir = ForwardAxis * vertical + RightAxis * horizontal;
        Vector3 translation = dir * MoveSpeed * Time.fixedDeltaTime;
        
        _rb.MovePosition(_rb.position + translation);
    }
}
