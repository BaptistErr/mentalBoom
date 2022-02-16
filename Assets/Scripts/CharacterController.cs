using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(
    typeof(CapsuleCollider), // this or any collider type, but since there are physics one must exist.
    typeof(Rigidbody)) // physics do control the character (gravity, any other forces, etc)
]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private GameManager manager;

    [SerializeField]
    private Image healthBar;

    private bool hit;
    public static CharacterController Instance;

    [SerializeField] private float _health = -1.0F;

    /// <summary> Base health of player </summary>
    public float Health => _health;
    public float MaxHealth;

#region Movements
[Header("Movements")]

    [SerializeField] private float _moveSpeed = 5.0F;
    /// <summary> The normal walk speed, without any other effect. </summary>
    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }
    
[Header("Dash")]
    [SerializeField] private float _dashMoveSpeed = 15.0F;
    /// <summary> The walk speed when dashing.
    /// The player cannot change its direction when a dash is performed. </summary>
    public float DashMoveSpeed
    {
        get => _dashMoveSpeed;
        set => _dashMoveSpeed = value;
    }

    [SerializeField] private float _dashDuration = 0.5F;
    /// <summary> The duration of a dash.
    /// When the character hits an obstacle, the dash immediately stops. </summary>
    public float DashDuration
    {
        get => _dashDuration;
        set => _dashDuration = value;
    }
    
    [SerializeField] private float _dashCooldown = 0.5F;
    /// <summary> How long does the player has to wait after a dash is finished. </summary>
    public float DashCooldown
    {
        get => _dashCooldown;
        set => _dashCooldown = value;
    }
    
[Header("Jump")]
    [SerializeField] private bool _isJumpEnabled = true;
    /// <summary> Is the jump globally enabled or not. </summary>
    public bool IsJumpEnabled
    {
        get => _isJumpEnabled;
        set => _isJumpEnabled = value;
    }
    
    
    [SerializeField] private float _jumpForce = 50.0F;
    /// <summary> The force applied when jumping (in newton). </summary>
    public float JumpForce
    {
        get => _jumpForce;
        set => _jumpForce = value;
    }
    
#endregion

#region Directions
    // The player movement referential won't be world's but camera's.
    //
    // Since the camera is not aligned to the world axis and the player movement referential must be
    // aligned to the camera, each axis must be redefined so the forward movement vector is perceptually
    // forward to the camera.
    
    // X Axis
    /// <summary> The left direction relative to the character's controls.
    /// Is is computed from the cross product of Forward and Up. </summary>
    public Vector3 LeftDirection => Vector3.Cross(ForwardDirection, UpDirection); // -X
    /// <summary> The right direction relative to the character's controls. </summary>
    public Vector3 RightDirection => -LeftDirection; // +X
        
    // Y Axis
    /// <summary> The up direction relative to the character's controls.
    /// This is a constant. </summary>
    public Vector3 UpDirection => Vector3.up; // +Y
    /// <summary> The down direction relative to the character's controls.
    /// This is a constant. </summary>
    public Vector3 DownDirection => -UpDirection; // -Y
    
    // Z Axis
    [SerializeField] private Vector3 _forwardDirection = (Vector3.left + Vector3.forward).normalized; // +Z (back field)
    /// <summary> The forward direction relative to the character's controls.
    /// this is the only setting to set, then all the other directions of the X and Z axis will adapt. </summary>
    public Vector3 ForwardDirection // +Z (API)
    {
        get => _forwardDirection;
        set => _forwardDirection = value;
    }
    /// <summary> The backward direction relative to the character's controls. </summary>
    public Vector3 BackwardDirection => -ForwardDirection; // -Z
    
#endregion

#region Attack

[Header("Attack")] 
    public Transform AttackZone;

    [SerializeField] private float _attackRange = 2.0F;
    /// <summary> The distance the player can attack enemies. </summary>
    public float AttackRange
    {
        get => _attackRange;
        set => _attackRange = value;
    }
    
    [SerializeField] private float _attackCooldown = 0.33F;
    /// <summary> The time to wait before another attack is possible. </summary>
    public float AttackCooldown
    {
        get => _attackCooldown;
        set => _attackCooldown = value;
    }

    [SerializeField] private float _attackLoadDuration = 2.0F;
    /// <summary> The time to press the key so an attach is full loaded. </summary>
    public float AttackLoadDuration
    {
        get => _attackLoadDuration;
        set => _attackLoadDuration = value;
    }
    
    [SerializeField] private AnimationCurve _attackDamageByLoad = new AnimationCurve();
    /// <summary> The damage dealt by the percentage of weapon load. </summary>
    public AnimationCurve AttackDamageByLoad
    {
        get => _attackDamageByLoad;
        set => _attackDamageByLoad = value;
    }
    
#endregion

    // component cache
    private Rigidbody _rb;
    private Collider _collider;
    
    // other
    private float _maxColliderExtent;
    private bool _characterOnFloor;

    // dash state
    private bool _dashing;
    private Vector3 _dashDir;
    private float _dashTime;
    private float _timeSinceDashEnd = float.PositiveInfinity;
    
    // attack
    //[SerializeField] private PlayerAttack _attackRecorder;
    private float _timeSinceAttack = float.PositiveInfinity;
    private float _attackLoadTime = 0.0F;

#region Unity callbacks

    private void Awake()
    {
        //_attackRecorder = gameObject.GetComponentInChildren<PlayerAttack>();
        //ResizeAttackZone(_attackRange);

        Instance = this;

        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        
        // get the max extent of the collider on the XZ plane for dash wall avoidance calculations
        Vector3 extents = _collider.bounds.extents;
        _maxColliderExtent = Math.Max(extents.x, extents.z);

        hit = false;
    }
    
    private void Update()
    {
        if (!_dashing)
        {
            _timeSinceDashEnd += Time.deltaTime;
            
            if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space)) && _timeSinceDashEnd >= _dashCooldown)
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

        _timeSinceAttack += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && _timeSinceAttack > _attackCooldown)
        {
            _attackLoadTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && _timeSinceAttack > _attackCooldown)
        {
            float weaponLoadPct = Math.Min(1.0F, _attackLoadTime / _attackLoadDuration);
            
            _timeSinceAttack = 0.0F;
            _attackLoadTime = 0.0F;
            
            Attack((int)_attackDamageByLoad.Evaluate(weaponLoadPct));
        }
    }
    
    private void FixedUpdate()
    {
        if (!manager.gameEnded)
        {
            if (_dashing)
            {
                if (Move(direction: _dashDir, speed: _dashMoveSpeed, considerMovables: true))
                { // if any object has been hit, stop dashing
                    StopDash();
                }
            }
            else
            {
                if (_isJumpEnabled && Input.GetButtonDown("Jump"))
                {
                    Jump(_jumpForce);
                }

                Move(direction: GetMoveDirection(), speed: _moveSpeed, considerMovables: false);
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Jumpable"))
        {
            _characterOnFloor = true;
        }
    }

    private void OnValidate()
    {
        //ResizeAttackZone(_attackRange);
    }

    #endregion

    /*private void ResizeAttackZone(float size)
    {
        if (_attackRecorder == null) return;
        
        Transform transform = _attackRecorder.transform;
        transform.localPosition = Vector3.forward * size / 2.0F;
        Vector3 scale = transform.localScale;
        scale.z = size;
        transform.localScale = scale;
    }*/
    
    private void Rotate(Vector3 forward)
    {
        transform.LookAt(transform.position + new Vector3(forward.x, 0.0F, forward.z));
    }

    private void Attack(int damage)
    {
        Collider[] colliders = Physics.OverlapBox(AttackZone.transform.position, AttackZone.transform.lossyScale / 2.0F, AttackZone.transform.rotation, 1<<11);
        foreach (Collider enemy in colliders)
        {
            enemy.gameObject.GetComponent<IEnemy>().GetDamage(damage);
        }
    }
    private bool Move(Vector3 direction, float speed, bool considerMovables)
    {
        Rotate(direction);
        float distanceToTravel = speed * Time.fixedDeltaTime;
        
        Vector3 translation = direction * distanceToTravel;
        bool doHit = _rb.SweepTest(direction, out RaycastHit hit, distanceToTravel, QueryTriggerInteraction.Ignore);
        if (doHit)
        {
            if (!considerMovables)
            {
                doHit = (hit.rigidbody == null);
            }
            else
            {
                doHit = IsObstacleOnFrame(direction, speed * 1.2F);
            }
            if (doHit) translation = direction * hit.distance;
        }
        _rb.MovePosition(_rb.position + translation);

        return doHit;
    }
    
    private void StartDash()
    {
        Vector3 dir = GetMoveDirection();
        if (dir != Vector3.zero)
        {
            _dashDir = dir;
            _dashing = true;
            _dashTime = 0.0F;
            GetDamage(0);
        }
    }
    private void StopDash()
    {
        _dashing = false;
        _timeSinceDashEnd = 0.0F;
    }

    private void Jump(float force)
    {
        if (_characterOnFloor)
        {
            _rb.AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
            _characterOnFloor = false;
        }
    }

    public enum ControlMode
    {
        Camera,
        World
    }

    public ControlMode _controlMode = ControlMode.Camera;
    private Vector3 GetMoveDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return _controlMode switch
        {
            ControlMode.Camera => (ForwardDirection*vertical + RightDirection*horizontal).normalized,
            ControlMode.World => (Vector3.forward*vertical+Vector3.right*horizontal).normalized
        };
    }
    
    private bool IsObstacleOnFrame(Vector3 direction, float speed)
    {
        if(_dashing) Debug.DrawRay(_rb.position, direction, Color.yellow, 1.0F);
        return Physics.Raycast(
            ray: new Ray(origin: _rb.position, direction),
            maxDistance: Time.fixedDeltaTime * speed + _maxColliderExtent,
            layerMask: ~(_collider.gameObject.layer | (1<<2))
        );
    }

    public void Heal(float heal)
    {

    }

    public void GetDamage(float damage)
    {
        if (!hit)
        {
            _health -= damage;
            healthBar.fillAmount = Health / MaxHealth;
            hit = true;
            StartCoroutine(WaitDamage());
        }
        if (Health <= 0)
        {
            transform.rotation = Quaternion.Euler(90, 0, 0);
            manager.GameEnded(false);
        }
    }

    IEnumerator WaitDamage()
    {
        yield return new WaitForSeconds(.5f);
        hit = false;
    }
}