// Imports
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

// Class; MonoBehaviour is an inherited base class for all Game Objects
public class PlayerController : MonoBehaviour
{
    public PauseMenuScript pms;
    public FireballScript fireball;
    public SwordScript sword;
    public ElementMenuScript ems;
    public Camera cam;

    private int Element = 0;
    private bool inMenu = false;
    private float cooldown = 10f;
    private float elapsedTime = 0f;
    private bool earthOnCD = false;

    // Normal variables
    private float moveSpeed = 10;
    private float jump = 15;
    private float dashSpeed = 3;
    private float gravity = 5;
    private float dashTime = 0.2f;
    private static int jumpCount = 0;
    private static bool isDashing = false;
    private static bool canDash = true;

    // Special variables; CharacterController is a class that allows for movement of an object that needs to be constrained by collisions
    private CharacterController controller;

    // Vector 3 is a struct that is a representation of vectors / points - used for manipulation of positions and moving directions
    private Vector3 moveDirection;

    // Start function; called only once in the life cycle of a funcion; second funcion called - only after awake (if applicable)
    // Start acts as an object initializer in Unity
    void Start()
    {
        pms.ResumeGame();
        ems.Hide(0);
        controller = GetComponent<CharacterController>();
        // GetComponent: gets a reference to a component of a given type T (CharacterController in this case) from the same GameObject
        // that the script is attached to (the "Player" Object in this case
        cam.transform.SetParent(controller.transform);
    }

    // Update Function: function that is called every frame that the given MonoBehavior is enabled (once a frame upon class initialization)
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !inMenu)
        {
            inMenu = true;
            ems.Show();
        }

        if (!sword.getCastable())
        {
            setElement(0);
            earthOnCD = true;
            StartCoroutine(Cooldown());
        }

        if (Input.GetButtonDown("Cancel") && !inMenu)
        {
            inMenu = true;
            pms.Pause();
        }

        // Temp var to store the current moveDirection's Y component (only the Y value of the Vector3)
        float yStore = moveDirection.y;

        /* 
         * Calculates the vertical and horizontal components of the movement direction; because moveDirection is a Vector-type variable,
         * it's values inherently store the direction in pos vs neg signs, the values actually indicate the speed the character is moving
         * in a given direction.
         * NOTE: vertical and horizontal are the X and Z components of the vector, meaning they do not control the up and down components
         */
        moveDirection = (transform.forward * Input.GetAxis("Vertical") * moveSpeed) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed);

        /*
         * normalized is a property of a Vector3 that recalculates the given vector to have a magnitude of 1
         * (magnitude is a fancy vector way of saying that it is the sum of the vetors)
         * normalization means that if a character moves in a non-cardinal direction, they will not move faster than they should
         */
        moveDirection = moveDirection.normalized * moveSpeed;

        // Y value is retored from temp variable; necessary because otherwise Y would contribute to vector normalization; meaning
        // that moving in a direciton and jumping would be slower than moving in a direction normally - not what we want
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

        if (Input.GetButtonDown("Fire2") && Element == 2 && !inMenu && !earthOnCD)
        {
            Debug.Log("Swing");
            sword.StartCoroutine(sword.Swing());
        }
        else if (Input.GetButtonDown("Fire2") && Element == 3 && !inMenu)
        {
            fireball.Cast(controller.transform.position, controller.transform.rotation);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (Element == 4 && jumpCount < 2 && !isDashing)
            {
                moveDirection.y = jump;
                jumpCount += 1;
            }
            else if (jumpCount < 1)
            {
                moveDirection.y = jump;
                jumpCount += 1;
            }
        }

        if (Input.GetButtonDown("Fire3") && !isDashing && canDash && Element == 4 && !inMenu)
        {
            canDash = false;
            StartCoroutine(Dash());
            isDashing = true;
        } else if (Input.GetButtonDown("Fire3") && Element == 1 && !inMenu)
        {
            // Water here
        }

        if (isDashing)
        {
            moveDirection.x *= dashSpeed;
            moveDirection.z *= dashSpeed;
            // deltaTime allows for the fps of the host to not impact the move speed of the character
            controller.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            // Calculates the gravity component of the vector so that gravity affects the character; gravity.y is the gravity constant
            // gravity is our own gravity multiplier
            moveDirection.y += (Physics.gravity.y * gravity * Time.deltaTime);

            // deltaTime allows for the fps of the host to not impact the move speed of the character
            controller.Move(moveDirection * Time.deltaTime);
        }

    }

    IEnumerator Cooldown()
    {
        while (elapsedTime < cooldown)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        earthOnCD = false;
        yield break;
    }

    public void setInMenu(bool inMenu)
    {
        this.inMenu = inMenu;
    }

    public void setElement(int Element)
    {
        if (Element == 2 && !earthOnCD && this.Element != 2)
        {
            StartCoroutine(sword.SwordTimer());
        }
        this.Element = Element;
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
