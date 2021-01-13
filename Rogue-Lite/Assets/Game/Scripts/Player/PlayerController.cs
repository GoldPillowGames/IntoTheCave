using System.Collections;
using System.Collections.Generic;
using GoldPillowGames.Player;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public enum PlayerState
{
    NEUTRAL,
    ATTACKING,
    BLOCKING,
    ROLLING,
    DIALOGUE,
    IS_BEING_DAMAGED,
    PAUSE_MENU
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
    public UIController UI;
    [Tooltip("Player animator")]
    [SerializeField] private Animator animator;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private FixedJoystick _leftJoystick;
    [SerializeField] private FixedJoystick _rightJoystick;
    public CameraFollower cameraFollower;
    [SerializeField] private PlayerWeaponController weapon;
    [SerializeField] private PlayerCrossbowController crossbow;

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
    [SerializeField] private LayerMask normalLayer;
    [SerializeField] private LayerMask rollingLayer;

    [Header("State Variables")]
    public int health;
    public int maxHealth = 100;

    [Header("Interaction Variables")]
    [SerializeField] private float checkRadius = 4f;
    [SerializeField] private float outRadius = 5f;

    [Header("Other")]
    [SerializeField] private bool debug = false;
    public MeleeWeaponTrail weaponTrail;


    public PlayerState playerState = PlayerState.NEUTRAL;

    [HideInInspector] public Quaternion currentRotation;     // Current looking rotation
    private CharacterController controller; // Character controller
    private Vector3 movement;               // Current Movement
    private Vector2 gravityVelocity;        // Gravity Velocity

    private bool isRolling = false;

    [HideInInspector] public PhotonView PV;
    [HideInInspector] public bool isMe;

    private void Awake()
    {
        PV = GetComponentInParent<PhotonView>();

        controller = GetComponent<CharacterController>();

        DontDestroyOnLoad(this);
        DontDestroyOnLoad(cam);
        DontDestroyOnLoad(cameraDirection);
        DontDestroyOnLoad(cameraFollower);

        if(transform.parent != null)
            DontDestroyOnLoad(transform.parent.gameObject);

        if(GameObject.FindGameObjectWithTag("Light"))
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Light"));
    }

    private Vector3 clickPosition;
    private Vector3 lookDirection;
    private bool canAttack = true;
    private bool canFinishAttack = true;
    private Vector3 rollDirection;
    [HideInInspector] public bool isDead = false;

    private bool canRoll = true;

    private bool interactableDetected = false;

    [HideInInspector] public bool    doorOpened    = false;
    [HideInInspector] public Vector3 doorDirection = new Vector3(0,0,0);
    [HideInInspector] public PlayerStatus playerStatus;

    [SerializeField] private TextMeshPro _nickname;
    [SerializeField] private TextMeshPro _itemDescription;

    private void Start()
    {
        playerStatus = this.GetComponent<PlayerStatus>();

        if (!PV.IsMine && Config.data.isOnline)
        {
            controller.enabled = false;
            cam.enabled = false;
            cam.GetComponent<AudioListener>().enabled = false;
            cam.GetComponent<CameraController>().cmCamera.enabled = false;

            Player[] players = PhotonNetwork.PlayerList;
            for (int i = 0; i < players.Length; i++)
            {
                if(players[i].NickName != PhotonNetwork.NickName)
                {
                    _nickname.text = players[i].NickName;
                }
            }
        }
        else if (PV.IsMine && Config.data.isOnline)
        {
            
            cam.GetComponent<CameraController>().SetFollowTarget(this.transform);
            GameManager.Instance.LoadGraphicsSettings(cam);
            cam.name = PhotonNetwork.NickName;
            _nickname.text = PhotonNetwork.NickName;
        }
        else if (!Config.data.isOnline)
        {
            GameManager.Instance.LoadGraphicsSettings(cam);
            _nickname.text = "";
        }

        isMe = PV.IsMine;

        playerStatus.UpdateStatus();

        maxHealth = playerStatus.health;
        health = maxHealth;
    }

    public void ThrowArrow()
    {
        crossbow.ThrowArrow();
    }
    
    public void ShowItemDescription(string description)
    {
        _itemDescription.text = description;
        _itemDescription.GetComponentInParent<Animator>().SetTrigger("ShowUp");
    }

    private Vector3 lastMovementDir = Vector3.zero;
    Quaternion originalRotation;
    Transform objectToSee;

    public void OpenPauseMenu()
    {
        FindObjectOfType<PauseMenuManager>().ActivatePauseMenu();
        if (!Config.data.isOnline)
        {
            FindObjectOfType<TimeManager>().timeScale = 0.0f;
            Time.timeScale = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine && Config.data.isOnline)
            return;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            OpenPauseMenu();
        }

        if (isDead)
            return;


        if (health <= 0)
        {
            print("Kill player");
            Kill();
            if (!Config.data.isOnline)
            {
                UI.deathMenuManager.PlayDeathMenu();
            }

        }

        maxHealth = playerStatus.health;
        

        // Updates where the player is looking to if he is moving
        if (((movement != Vector3.zero || playerState == PlayerState.ATTACKING) && playerState != PlayerState.BLOCKING) || doorOpened)
        {
            playerContainer.rotation = Quaternion.Lerp(playerContainer.rotation, currentRotation, rotationSpeed * Time.deltaTime);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius, whatIsInteractable);

        if(hitColliders.Length > 0)
        {
            interactableDetected = true;
            foreach(Collider col in hitColliders)
            {
                NPCBehaviour npc = col.GetComponent<NPCBehaviour>();
                if (npc)
                {
                    npc.ShowInteractBox();
                }
            }
        }

        Collider[] hitColliders2 = Physics.OverlapSphere(transform.position, outRadius, whatIsInteractable);

        if (hitColliders2.Length == 0 && interactableDetected)
        {
            interactableDetected = false;
        }

        if (interactableDetected)
        {
            if(cameraController.cameraState != CameraState.DIALOGUE)
            cameraController.cameraState = CameraState.INTERACT;
        }
        else if(cameraController.cameraState != CameraState.END_ROOM)
        {
            cameraController.cameraState = CameraState.IDLE;
        }

        #region Input
        if (Input.GetMouseButton(1) && !Config.data.isTactile && playerState == PlayerState.NEUTRAL || playerState == PlayerState.BLOCKING)
        {
            animator.SetBool("IsDefending", true);
            playerState = PlayerState.BLOCKING;
        }
        else
        {
            animator.SetBool("IsDefending", false);
        }

        if (Input.GetMouseButtonUp(1) && !Config.data.isTactile && playerState == PlayerState.BLOCKING)
        {
            playerState = PlayerState.NEUTRAL;
        }

        

        if (((Input.GetMouseButton(0) && !Config.data.isTactile) /*|| (_rightJoystick.Horizontal != 0 || _rightJoystick.Vertical != 0)*/) /*&& canAttack*/ && _timeToAttack <= 0 && (playerState == PlayerState.NEUTRAL || playerState == PlayerState.ATTACKING) /*&& !animator.GetBool("HasAttackedBool")*/ )
        {
            canAttack = false;
            canFinishAttack = false;
            playerState = PlayerState.ATTACKING;

            print("Attacking");
            animator.SetTrigger("Attack1");
            animator.SetBool("Attack1Bool", true);
            //animator.SetBool("HasAttackedBool", true);
            animator.SetBool("IsAttacking", true);
            
            // Attack Variables
            //print(_timeToAttack);
            _timeToAttack = _attackTime[_attackIndex];
            _timeToMove = _moveTime[_attackIndex];
            // _attackIndex = _attackIndex == numberOfAttacks - 1 ? 0 : _attackIndex+1;
            print(_attackIndex);

            if (!Config.data.isTactile)
            {
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
            else
            {
                // Vector3 mousePos = Input.mousePosition;
                // clickPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                // lookDirection = new Vector3(-_rightJoystick.Horizontal, 0, -_rightJoystick.Vertical);
                // currentRotation = Quaternion.LookRotation(lookDirection.normalized, transform.up);
            }

            
        }

        if(playerState == PlayerState.ROLLING)
        {
            this.gameObject.layer = 15;
        }
        else if (!isDead)
        {
            this.gameObject.layer = 10;
            canRoll = true;
            animator.SetBool("IsRolling", false);

        }

        

        if (rollTimer <= 0 && Input.GetKeyDown(KeyCode.Space) && playerStatus.canRoll && movement != Vector3.zero && playerState == PlayerState.NEUTRAL && canRoll && !Config.data.isTactile)
        {
            playerState = PlayerState.ROLLING;
            rollDirection = movement;
            currentRotation = Quaternion.LookRotation(rollDirection);
            movement = Vector3.zero;
            canRoll = false;
            animator.SetBool("IsRolling", true);
            rollTimer = maxRollTimer;
            StartRoll();
        }
        #endregion

        Movement();

        if (playerState == PlayerState.IS_BEING_DAMAGED && _pushTime > 0)
        {
            animator.SetBool("IsBeingDamaged", true);
            controller.Move(_pushSpeed * _pushDirection * Time.deltaTime);
            _pushTime -= Time.deltaTime;
        }
        else if(playerState == PlayerState.IS_BEING_DAMAGED)
        {
            animator.SetBool("IsBeingDamaged", false);
            playerState = PlayerState.NEUTRAL;
        }

        if (isTactileAttacking)
        {
            TactileAttack();
        }
    }

    public float rollTimer;
    private float maxRollTimer = 0.5f;

    public bool isTactileAttacking = false;

    public void TactileAttack()
    {
        if (_timeToAttack <= 0 && (playerState == PlayerState.NEUTRAL))
        {
            canAttack = false;
            canFinishAttack = false;
            playerState = PlayerState.ATTACKING;

            print("Attacking");
            animator.SetTrigger("Attack1");
            animator.SetBool("Attack1Bool", true);
            //animator.SetBool("HasAttackedBool", true);
            animator.SetBool("IsAttacking", true);

            // Attack Variables
            //print(_timeToAttack);
            _timeToAttack = _attackTime[_attackIndex];
            _timeToMove = _moveTime[_attackIndex];
            // _attackIndex = _attackIndex == numberOfAttacks - 1 ? 0 : _attackIndex+1;
            print(_attackIndex);

            lookDirection = lastMovementDir;
            currentRotation = Quaternion.LookRotation(lookDirection.normalized, transform.up);
        }
        
    }

    public void SpecialSkillDown()
    {
        if (!Config.data.isTactile && playerState == PlayerState.NEUTRAL || playerState == PlayerState.BLOCKING)
        {
            animator.SetBool("IsDefending", true);
            playerState = PlayerState.BLOCKING;
        }
        else
        {
            animator.SetBool("IsDefending", false);
        }
    }

    public void SpecialSkillUp()
    {
        if(!Config.data.isTactile && playerState == PlayerState.BLOCKING)
        {
            playerState = PlayerState.NEUTRAL;
        }
    }

    public void Roll()
    {
        if (rollTimer <= 0 && playerStatus.canRoll && movement != Vector3.zero && playerState == PlayerState.NEUTRAL && canRoll)
        {
            playerState = PlayerState.ROLLING;
            rollDirection = lastMovement;
            currentRotation = Quaternion.LookRotation(rollDirection);
            movement = Vector3.zero;
            canRoll = false;
            animator.SetBool("IsRolling", true);
            rollTimer = maxRollTimer;
            StartRoll();
        }
    }

    public void SetAttackIndex(int index)
    {
        _attackIndex = index;
    }

    private float _pushTime;
    [SerializeField] private float _maxPushTime = 0.10f;
    private Vector3 _pushDirection;
    private float _pushSpeed = 8;

    public void TakeDamage(int damage, Vector3 pushDirection)
    {
        if ((!PV.IsMine && Config.data.isOnline) || playerState == PlayerState.BLOCKING || playerState == PlayerState.ROLLING)
        {
            print("Not me");
            return;
        }
            

        int calculatedDamage = damage;

        if (!Config.data.isOnline)
            calculatedDamage = damage * Config.data.dungeonLevel;
        else
            calculatedDamage = damage;

        print("Damage: " + damage);
        print("Calculated Damage: " + calculatedDamage);

        playerState = PlayerState.IS_BEING_DAMAGED;
        LetAttack();
        if (health - calculatedDamage < 1 && health > 1 && playerStatus.survivesToLetalAttack)
        {
            health = 1; 
            playerStatus.survivesToLetalAttack = false;
        }
        else
        {
            health -= calculatedDamage;
        }

        

        CameraShaker.Shake(0.2f, 2f, 1.75f);

        animator.SetBool("IsBeingDamaged", true);

        this._pushDirection = pushDirection;
        _pushTime = _maxPushTime;
    }


    public float[] _attackTime;
    public float[] _moveTime;
    public int numberOfAttacks = 3;
    private float _timeToAttack = 0;
    private float _timeToMove = 0;
    private int _attackIndex = 0;

    private void FixedUpdate()
    {
        if (!PV.IsMine && Config.data.isOnline)
            return;

        if (isDead)
            return;

        if (rollTimer > 0)
        {
            rollTimer -= Time.deltaTime;
        }

        _timeToAttack -= Time.deltaTime;

        if (_timeToAttack <= 0)
        {
            animator.SetBool("HasAttackedBool", false);
            animator.SetBool("Attack1Bool", false);
        }
        else
        {
            animator.SetBool("HasAttackedBool", true);
            animator.SetBool("Attack1Bool", true);
        }


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
        if (!Config.data.isOnline && !isDead)
        {
            isDead = true;
            Config.data.dungeonsStarted++;
            animator.SetTrigger("Death");
            
            // UI.ShowDeathMenu();
        }
        else if(!isDead)
        {
            isDead = true;
            PV.RPC("ShowDeathMenu", RpcTarget.All);
        }
        
    }

    

    public void Disconnect()
    {
        PV.RPC("DisconnectPlayer", RpcTarget.All);
    }

    public void Revive()
    {
        Fade.OnPlay = GameManager.Instance.EndRun;
        Fade.PlayFade(FadeType.INSTANT);
    }
    Vector3 lastMovement;
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
                lastMovementDir = movement.normalized;
            }
            else
            {
                Vector2 input = new Vector2(_leftJoystick.Horizontal, _leftJoystick.Vertical).normalized;
                movement = (input == Vector2.zero) ? Vector3.zero : cameraDirection.right.normalized * input.x + cameraDirection.forward.normalized * input.y;
                if(input != Vector2.zero)
                {
                    lastMovement = movement;
                }
                    //lastMovementDir = movement.normalized;
            }

            if (playerState == PlayerState.NEUTRAL)
            {
                // _attackIndex = 0;
                controller.Move(movement * speed * playerStatus.movementSpeed * Time.deltaTime);
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
                // _attackIndex = 0;
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

    public void FinishAttackInWeapon()
    {
        weapon.FinishAttack();
    }

    public void SetNewCurrentWeapon(PlayerWeaponController newCurrentWeapon)
    {
        weapon = newCurrentWeapon;
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
