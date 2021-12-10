using System;
using UnityEngine;

[RequireComponent(
    typeof(CapsuleCollider), // this or any collider type, but since there are physics one must exist.
    typeof(Rigidbody)) // physics do control the character (gravity, any other forces, etc)
]
public class CharacterController : MonoBehaviour
{
    
#region Movement

    [SerializeField] private float _moveSpeed = 5.0F;
    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }
    
    [SerializeField] private float _dashMoveSpeed = 15.0F;
    public float DashMoveSpeed
    {
        get => _dashMoveSpeed;
        set => _dashMoveSpeed = value;
    }
    [SerializeField] private float _dashDuration = 0.5F;
    public float DashDuration
    {
        get => _dashDuration;
        set => _dashDuration = value;
    }
    
    [SerializeField] private float _dashCooldown = 0.5F;
    public float DashCooldown
    {
        get => _dashCooldown;
        set => _dashCooldown = value;
    }
    
#endregion

#region Directions
    // The player movement referential won't be world's but camera's.
    //
    // Since the camera is not aligned to the world axis and the player movement referential must be
    // aligned to the camera, each axis must be redefined so the forward movement vector is perceptually
    // forward to the camera.
    
    // X Axis
    public Vector3 LeftDirection => Vector3.Cross(ForwardDirection, UpDirection); // -X
    public Vector3 RightDirection => -LeftDirection; // +X
        
    // Y Axis
    public Vector3 UpDirection => Vector3.up; // +Y
    public Vector3 DownDirection => -UpDirection; // -Y
    
    // Z Axis
    [SerializeField] private Vector3 _forwardDirection = (Vector3.left + Vector3.forward).normalized; // +Z (back field)
    public Vector3 ForwardDirection // +Z (API)
    {
        get => _forwardDirection;
        set => _forwardDirection = value;
    }
    public Vector3 BackwardDirection => -ForwardDirection; // -Z
    
#endregion

    private Rigidbody _rb; // rigidbody cache, no GetComponent<T>() spam, ugh.
    private Collider _collider; // idem.
    private float _maxColliderExtent;

    // dash state
    private bool _dashing;
    private Vector3 _dashDir;
    private float _dashTime;
    private float _timeSinceDashEnd = float.PositiveInfinity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        
        // get the max extent of the collider on the XZ plane for dash wall avoidance calculations
        Vector3 extents = _collider.bounds.extents;
        _maxColliderExtent = Math.Max(extents.x, extents.z);
    }

    private void Update()
    {
        if (!_dashing)
        {
            _timeSinceDashEnd += Time.deltaTime;
            
            if (Input.GetKeyDown(KeyCode.LeftShift) && _timeSinceDashEnd >= _dashCooldown)
            {
                StartDash();
            }
        }
        else
        {
            _dashTime += Time.deltaTime;
            
            if (_dashTime >= _dashDuration)
            {
                StopDash();
            }
        }
    }

    private void FixedUpdate()
    {
        if (_dashing)
        {
            Ray wallTestRay = new Ray(origin: _rb.position, direction: _dashDir);
            float checkDistance = Time.fixedDeltaTime*_dashMoveSpeed + _maxColliderExtent;

            if (Physics.Raycast(ray: wallTestRay, maxDistance: checkDistance, layerMask: ~_collider.gameObject.layer))
            { // if any object has been hit, stop dashing
                StopDash();
            }
            else
            {
                Move(direction: _dashDir, speed: _dashMoveSpeed);
            }
        }
        else
        {
            Move(direction: GetMoveDirection(), speed: _moveSpeed);
        }
    }

    private void Move(Vector3 direction, float speed)
    {
        Vector3 dir = GetMoveDirection();
        Vector3 translation = direction * speed * Time.fixedDeltaTime;
        
        _rb.MovePosition(_rb.position + translation);
    }
    
    private void StartDash()
    {
        Vector3 dir = GetMoveDirection();
        if (dir != Vector3.zero)
        {
            _dashDir = GetMoveDirection();
            _dashing = true;
            _dashTime = 0.0F;
        }
    }
    private void StopDash()
    {
        _dashing = false;
        _timeSinceDashEnd = 0.0F;
    }

    private Vector3 GetMoveDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return (ForwardDirection*vertical + RightDirection*horizontal).normalized;
    }
}
