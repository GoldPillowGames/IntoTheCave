using System.Collections;
using System.Collections.Generic;
using GoldPillowGames.Player;
using UnityEngine;

public enum PlayerState
{
    NEUTRAL,
    ATTACKING,
    BLOCKING,
    ROLLING,
    DIALOGUE
}
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Main camera reference")]
    [SerializeField] private Camera cam;
    [Tooltip("Camera orientation transform")]
    [SerializeField] private Transform cameraDirection;
    [Tooltip("Ground checker transform")]
    [SerializeField] private Transform groundChecker;
    [Tooltip("Player container reference")]
    [SerializeField] private Transform playerContainer;
    [Tooltip("UI reference")]
    [SerializeField] private UIController UI;
    [Tooltip("Player animator")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject lights;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private FixedJoystick joystick;
    public CameraFollower cameraFollower;
    [SerializeField] private PlayerWeaponController weapon;

    [Header("Movement Variables")]
    [Tooltip("Player movement speed")]
    [SerializeField] private float speed = 5;
    [Tooltip("Gravity force strength")]
    [SerializeField] private float gravity = -27.5f;
    [Tooltip("Ground checker radius")]
    [SerializeField] private float groundCheckerArea;
    [Tooltip("Ground Layer Mask")]
    [SerializeField] private LayerMask whatIsGround;
    [Tooltip("Interactable Layer Mask")]
    [SerializeField] private LayerMask whatIsInteractable;
    [Tooltip("Cursor Click Layer Mask")]
    [SerializeField] private LayerMask whatIsCursorClick;
    [Tooltip("Player rotation speed")]
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float attackDashTimer;
    [SerializeField] private float attackMaxDashTimer = 0.1f;
    [SerializeField] private float timeToDash;
    [SerializeField] private float maxTimeToDash = 0.05f;

    [Header("State Variables")]
    public int health;
    public int maxHealth = 100;

    [Header("Interaction Variables")]
    [SerializeField] private float checkRadius = 4f;
    [SerializeField] private float outRadius = 5f;

    [Header("Other")]
    [SerializeField] private bool debug = false;
    public MeleeWeaponTrail weaponTrail;

    

    

    private PlayerState playerState = PlayerState.NEUTRAL;

    [HideInInspector] public Quaternion currentRotation;     // Current looking rotation
    private CharacterController controller; // Character controller
    private Vector3 movement;               // Current Movement
    private Vector2 gravityVelocity;        // Gravity Velocity

    private bool isRolling = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(cam);
        DontDestroyOnLoad(lights);
        DontDestroyOnLoad(cameraDirection);
        DontDestroyOnLoad(cameraFollower);
    }

    private Vector3 clickPosition;
    private Vector3 lookDirection;
    private bool canAttack = true;
    private bool canFinishAttack = true;
    private Vector3 rollDirection;
    private bool isDead = false;

    private bool canRoll = true;

    private bool interactableDetected = false;

    [HideInInspector] public bool    doorOpened    = false;
    [HideInInspector] public Vector3 doorDirection = new Vector3(0,0,0);

    private void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
            return;
        
        Movement();

        // Updates where the player is looking to if he is moving
        if (((movement != Vector3.zero || playerState == PlayerState.ATTACKING) && playerState != PlayerState.BLOCKING) || doorOpened)
        {
            playerContainer.rotation = Quaternion.Lerp(playerContainer.rotation, currentRotation, rotationSpeed * Time.deltaTime);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius, whatIsInteractable);

        if(hitColliders.Length > 0)
        {
            interactableDetected = true;
        }

        Collider[] hitColliders2 = Physics.OverlapSphere(transform.position, outRadius, whatIsInteractable);

        if (hitColliders2.Length == 0 && interactableDetected)
        {
            interactableDetected = false;
        }

        if (interactableDetected)
        {
            cameraController.cameraState = CameraState.INTERACT;
        }
        else if(cameraController.cameraState != CameraState.END_ROOM)
        {
            cameraController.cameraState = CameraState.IDLE;
        }

        #region Input
        if (Input.GetMouseButton(1) && !Config.data.isTactile)
        {
            animator.SetBool("IsDefending", true);
            playerState = PlayerState.BLOCKING;
        }
        else
        {
            animator.SetBool("IsDefending", false);
        }

        if (Input.GetMouseButtonUp(1) && !Config.data.isTactile)
        {
            playerState = PlayerState.NEUTRAL;
        }

        if (Input.GetMouseButton(0) /*&& canAttack*/ && _timeToAttack <= 0 && !Config.data.isTactile)
        {
            canAttack = false;
            canFinishAttack = false;
            playerState = PlayerState.ATTACKING;
            
            animator.SetTrigger("Attack1");
            animator.SetBool("IsAttacking", true);

            // Attack Variables
            print(_timeToAttack);
            _timeToAttack = _attackTime[_attackIndex];
            _timeToMove = _moveTime[_attackIndex];
            _attackIndex = _attackIndex == numberOfAttacks - 1 ? 0 : _attackIndex+1;
            

            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit cameraRayHit;
            if (Physics.Raycast(cameraRay, out cameraRayHit, 100000, whatIsCursorClick))
            {
                clickPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                lookDirection = (clickPosition - transform.position).normalized;
                currentRotation = Quaternion.LookRotation(lookDirection.normalized, transform.up);
            }

            Vector3 mousePos = Input.mousePosition;
            mousePos.Normalize();
        }

        if (Input.GetKeyDown(KeyCode.Space) && movement != Vector3.zero && playerState == PlayerState.NEUTRAL && canRoll && !Config.data.isTactile)
        {
            playerState = PlayerState.ROLLING;
            rollDirection = movement;
            currentRotation = Quaternion.LookRotation(rollDirection);
            movement = Vector3.zero;
            canRoll = false;
            animator.SetBool("IsRolling", true);
            StartRoll();
        }
        #endregion

        _timeToAttack -= Time.deltaTime;
        _timeToMove -= Time.deltaTime;
        if (_timeToMove <= 0)
        {
            animator.SetBool("IsAttacking", false);
            // canFinishAttack = true;
            if (playerState == PlayerState.ATTACKING)
            {
                playerState = PlayerState.NEUTRAL;
                if (movement != Vector3.zero)
                {
                    if (playerState == PlayerState.NEUTRAL)
                    {
                        animator.SetBool("IsWalking", true);
                        currentRotation = Quaternion.LookRotation(movement);
                    }
                }
                else
                {
                    animator.SetBool("IsWalking", false);
                }
            }
        }
    }

    public float[] _attackTime;
    public float[] _moveTime;
    public int numberOfAttacks = 3;
    private float _timeToAttack = 0;
    private float _timeToMove = 0;
    private int _attackIndex = 0;

    private void FixedUpdate()
    {
        
    }

    #region Events
    public void StartRoll()
    {
        isRolling = true;
    }

    public void EndRoll()
    {
        isRolling = false;
        playerState = PlayerState.NEUTRAL;
        animator.SetBool("IsRolling", false);
    }

    public void LetRoll()
    {
        canRoll = true;
    }

    private float attackDistance;

    public void Attack(float attackDistance)
    {
        attackDashTimer = attackMaxDashTimer;
        this.attackDistance = attackDistance;
        weaponTrail.Emit = true;
        animator.SetBool("IsWalking", false);
    }

    public void LetAttack()
    {
        //canAttack = true;
        //canFinishAttack = true;
        weaponTrail.Emit = false;
    }

    public void FinishAttack()
    {
        if (canFinishAttack)
        {
            
        }
        
    }
    #endregion

    public void Kill()
    {
        isDead = true;
        animator.SetTrigger("Death");
    }

    public void Revive()
    {
        isDead = false;
        health = maxHealth;
        animator.SetTrigger("Revive");
    }

    /// <summary>
    /// Calculates and updates player movement
    /// </summary>
    private void Movement()
    {
        if (!doorOpened)
        {
            #region Horizontal Movement Calculation & Assignation
            if (!Config.data.isTactile)
            {
                Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
                movement = (input == Vector2.zero) ? Vector3.zero : cameraDirection.right.normalized * input.x + cameraDirection.forward.normalized * input.y;
            }
            else
            {
                Vector2 input = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
                movement = (input == Vector2.zero) ? Vector3.zero : cameraDirection.right.normalized * input.x + cameraDirection.forward.normalized * input.y;
            }

            if (playerState == PlayerState.NEUTRAL)
            {
                _attackIndex = 0;
                controller.Move(movement * speed * Time.deltaTime);
            }

            #endregion

        }
        else
        {
            movement = Vector3.zero;
            // print
            animator.SetBool("IsWalking", true);
            _attackIndex = 0;
            controller.Move(doorDirection * speed * Time.deltaTime);
        }


        // Calculate where does the player looks
        if (movement != Vector3.zero)
        {
            if (playerState == PlayerState.NEUTRAL)
            {
                canAttack = true;
                animator.SetBool("IsWalking", true);
                currentRotation = Quaternion.LookRotation(movement);
            }
        }
        else if(!doorOpened)
        {
            animator.SetBool("IsWalking", false);
        }


        if (attackDashTimer > 0)
        {
            controller.Move(lookDirection * attackDistance * Time.deltaTime);
            attackDashTimer -= Time.deltaTime;
        }


        if (playerState == PlayerState.ROLLING && isRolling)
        {
            controller.Move(rollDirection * 29 * Time.deltaTime);
        }

        // If player is touching the floor, gravity force is not applied
        gravityVelocity.y = (IsGrounded() && gravityVelocity.y < 0) ? 0 : gravityVelocity.y + gravity * Time.deltaTime;
        controller.Move(gravityVelocity * Time.deltaTime);
    }

    /// <summary>
    /// Checks if player is touching the floor.
    /// </summary>
    /// <returns>Player is touching floor?</returns>
    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundChecker.position, groundCheckerArea, whatIsGround);
    }

    public void InitAttackInWeapon()
    {
        weapon.InitAttack();
    }
    
    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(clickPosition, 1);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundChecker.position, groundCheckerArea);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
