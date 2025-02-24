// Imports
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.InputSystem;

// Class; MonoBehaviour is an inherited base class for all Game Objects
public class PlayerInputController : MonoBehaviour
{
    // Normal variables
    private float moveSpeed = 10;
    private float jump = 15;
    private float dashSpeed = 3;
    private float gravity = 5;
    private float dashTime = 0.2f;
    private static int jumpCount = 0;
    private static bool isDashing = false;
    private static bool canDash = true;
    private Vector3 moveDirection;
    private Vector2 moveDir2;

    public Camera camera;



    // Special variables; CharacterController is a class that allows for movement of an object that needs to be constrained by collisions
    private CharacterController controller;
    private PlayerControls playerControls;

    // Vector 3 is a struct that is a representation of vectors / points - used for manipulation of positions and moving directions
    

    // Start function; called only once in the life cycle of a funcion; second funcion called - only after awake (if applicable)
    // Start acts as an object initializer in Unity
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerControls.Enable();
    }

    // Update Function: function that is called every frame that the given MonoBehavior is enabled (once a frame upon class initialization)
    void Update()
    {
        float yStore = moveDirection.y;

        moveDirection *= moveSpeed;
        moveDirection = moveDirection.normalized * moveSpeed;

        moveDirection.y = yStore;


        if (controller.isGrounded)
        {
            if (!isDashing)
            {
                canDash = true;
            }
            moveDirection.y = 0f;
            jumpCount = 0;
        }

        if (isDashing)
        {
            moveDirection.x *= dashSpeed;
            moveDirection.z *= dashSpeed;
            // deltaTime allows for the fps of the host to not impact the move speed of the character
            moveDirection.x *= camera.transform.forward.x;
            controller.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            // Calculates the gravity component of the vector so that gravity affects the character; gravity.y is the gravity constant
            // gravity is our own gravity multiplier
            moveDirection.y += (Physics.gravity.y * gravity * Time.deltaTime);

            // deltaTime allows for the fps of the host to not impact the move speed of the character
            moveDirection.x *= camera.transform.right.x;
            moveDirection.z *= camera.transform.forward.z;
            moveDirection = moveDirection.normalized * moveSpeed;
            controller.Move(moveDirection * Time.deltaTime);
        }

    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDir2 = context.ReadValue<Vector2>();
        moveDirection.x = moveDir2.x;
        moveDirection.z = moveDir2.y;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpCount < 2 && !isDashing)
        {
            moveDirection.y = jump;
            jumpCount += 1;
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (!isDashing && canDash)
        {
            canDash = false;
            StartCoroutine(Dash());
            isDashing = true;
        }
    }

    // Coroutine function; acts as a function that can "run in the background" allowing for other functions like Update() to execute while still keeping a timer running
    // Note: this cannot be done with increments in the update function or similar because it varies for those with different fps; the WaitForSeconds class uses time similar to deltaTime
    private IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        yield break;
    }
}
