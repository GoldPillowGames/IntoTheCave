using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Main camera reference")]
    [SerializeField] private Camera cam;
    [Tooltip("Camera orientation transform")]
    [SerializeField] private Transform cameraDirection;
    [Tooltip("Player movement speed")]
    [SerializeField] private float speed = 5;
    [Tooltip("Gravity force strength")]
    [SerializeField] private float gravity = -27.5f;
    [Tooltip("Ground checker transform")]
    [SerializeField] private Transform groundChecker;
    [Tooltip("Ground checker radius")]
    [SerializeField] private float groundCheckerArea;
    [Tooltip("Ground Layer Mask")]
    [SerializeField] private LayerMask whatIsGround;
    [Tooltip("Player container reference")]
    [SerializeField] private Transform playerContainer;
    [Tooltip("UI reference")]
    [SerializeField] private UIController UI;
    [Tooltip("Player rotation speed")]
    [SerializeField] private float rotationSpeed = 15f;

    private Quaternion currentRotation;     // Current looking rotation
    private CharacterController controller; // Character controller
    private Vector3 movement;               // Current Movement
    private Vector2 gravityVelocity;        // Gravity Velocity

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        // Updates where the player is looking to if he is moving
        if (movement != Vector3.zero)
            playerContainer.rotation = Quaternion.Lerp(playerContainer.rotation, currentRotation, rotationSpeed * Time.deltaTime);
    }

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
        controller.Move(movement * speed * Time.deltaTime);
        #endregion

        // Calculate where does the player looks
        if(movement != Vector3.zero)
            currentRotation = Quaternion.LookRotation(movement);

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundChecker.position, groundCheckerArea);
    }
}
