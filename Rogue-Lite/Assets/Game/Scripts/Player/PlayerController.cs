using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private CameraController cameraController;

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

    [Header("Interaction Variables")]
    [SerializeField] private float checkRadius = 4f;
    [SerializeField] private float outRadius = 5f;

    [Header("Other")]
    [SerializeField] private bool debug = false;
    [SerializeField] private MeleeWeaponTrail weaponTrail;

    

    

    private PlayerState playerState = PlayerState.NEUTRAL;

    private Quaternion currentRotation;     // Current looking rotation
    private CharacterController controller; // Character controller
    private Vector3 movement;               // Current Movement
    private Vector2 gravityVelocity;        // Gravity Velocity

    private bool isRolling = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    private Vector3 clickPosition;
    private Vector3 lookDirection;
    private bool canAttack = true;
    private bool canFinishAttack = true;
    private Vector3 rollDirection;

    private bool canRoll = true;

    private bool interactableDetected = false;
    // Update is called once per frame
    void Update()
    {
        Movement();

        // Updates where the player is looking to if he is moving
        if ((movement != Vector3.zero || playerState == PlayerState.ATTACKING) && playerState != PlayerState.BLOCKING)
            playerContainer.rotation = Quaternion.Lerp(playerContainer.rotation, currentRotation, rotationSpeed * Time.deltaTime);

        // RaycastHit interactableHit;
         // = Physics.SphereCast(transform.position, checkRadius, transform.forward, out interactableHit, checkRadius, whatIsInteractable);

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
        //foreach (var hitCollider in hitColliders)
        //{
        //    print("Interactable");
        //    //hitCollider.SendMessage("AddDamage");
        //    if (hitCollider.gameObject.layer == 9)
        //    {
        //        interactableDetected = true;
        //        print("Interactable");
        //    }
        //}


        if (interactableDetected)
        {
            cameraController.cameraState = CameraState.INTERACT;
        }
        else
        {
            cameraController.cameraState = CameraState.IDLE;
        }

        #region Input
        if (Input.GetMouseButton(1))
        {
            animator.SetBool("IsDefending", true);
            playerState = PlayerState.BLOCKING;
        }
        else
        {
            animator.SetBool("IsDefending", false);
        }

        if (Input.GetMouseButtonUp(1))
        {
            playerState = PlayerState.NEUTRAL;
        }

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            canAttack = false;
            canFinishAttack = false;
            playerState = PlayerState.ATTACKING;
            animator.SetBool("IsWalking", false);
            animator.SetTrigger("Attack1");
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

        if (Input.GetKeyDown(KeyCode.Space) && movement != Vector3.zero && playerState == PlayerState.NEUTRAL && canRoll)
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
    }

    public void LetAttack()
    {
        canAttack = true;
        canFinishAttack = true;
        weaponTrail.Emit = false;
    }

    public void FinishAttack()
    {
        if (canFinishAttack)
        {
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
    #endregion

    void HandleMouseMovement()
    {
        //if (Input.GetMouseButtonDown(0) && !cursorClicking)
        //{
        //    cursorClicking = true;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    //if (Physics.Raycast(ray))
        //    //    Instantiate(particle, transform.position, transform.rotation);
        //    clickPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //    UI.ShowInput();
        //    cameraDirection.GetComponent<MovementOrientation>().canUpdatePos = false;
        //    //Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        //}

        //if(Input.GetMouseButtonUp(0) && cursorClicking)
        //{
        //    cursorClicking = false;
        //    cameraDirection.GetComponent<MovementOrientation>().canUpdatePos = true;
        //    UI.HideInput();
        //    movementDirection = Vector2.zero;
        //}

        //if (cursorClicking)
        //{
            
        //    Vector2 currentPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //    movementDirection = (currentPosition - clickPosition).normalized;
        //    UI.SetMousePos(clickPosition, currentPosition);

        //}
    }

    /// <summary>
    /// Calculates and updates player movement
    /// </summary>
    private void Movement()
    {
        #region Horizontal Movement Calculation & Assignation
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        movement = (input == Vector2.zero) ? Vector3.zero : cameraDirection.right.normalized * input.x + cameraDirection.forward.normalized * input.y;
        
        if(playerState == PlayerState.NEUTRAL)
            controller.Move(movement * speed * Time.deltaTime);
        #endregion

        // Calculate where does the player looks
        if(movement != Vector3.zero)
        {
            if (playerState == PlayerState.NEUTRAL)
            {
                canAttack = true;
                animator.SetBool("IsWalking", true);
                currentRotation = Quaternion.LookRotation(movement);
            }
        }
        else
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
