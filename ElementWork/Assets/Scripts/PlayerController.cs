// Imports
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Class; MonoBehaviour is an inherited base class for all Game Objects
public class PlayerController : MonoBehaviour
{
    public PauseMenuScript pms;
    public FireballScript fireball;
    public SwordScript sword;
    public ElementMenuScript ems;
    public WaterScript water;

    public Camera cam;
    public Image earthAlpha;

    public GameObject prefab;
    public GameObject player;
    public GameObject armature;

    public CapsuleCollider capColl;

    private GameObject piece1;
    private GameObject piece2;

    private Animator animator;

    private GameObject[] pieces;

    private int element = 0;

    private bool inMenu = false;
    private bool earthOnCD = false;
    private bool raycastFinished = false;
    private bool bridgeActive = false;
    private bool piece1Moving = false;
    private bool piece2Moving = false;
    private bool swordActive = false;
    public bool isWalking = false;

    private readonly float cooldown = 7f;
    private readonly float swordDuration = 15f;
    private readonly float bridgeDuration = 5f;
    private readonly float moveTime = 0.2f;
    private float earthCDTimer = 0f;
    private float swordTimer = 0f;
    private float bridgeTimer = 0f;
    private float piece1Time = 0f;
    private float piece2Time = 0f;



    private readonly float moveSpeed = 10f;
    private readonly float jump = 17f;
    private readonly float doubleJump = 22f;
    private readonly float dashSpeed = 3f;
    private readonly float gravity = 5f;
    private readonly float dashTime = 0.3f;

    private int jumpCount = 0;

    private bool isDashing = false;
    private bool canDash = true;

    private CharacterController controller;

    private Vector3 moveDirection;

    // Start function; called only once in the life cycle of a funcion; second funcion called - only after awake (if applicable)
    // Start acts as an object initializer in Unity
    void Start()
    {
        pms.ResumeGame();
        ems.Hide(0);
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        // GetComponent: gets a reference to a component of a given type T (CharacterController in this case) from the same GameObject
        // that the script is attached to (the "Player" Object in this case
        cam.transform.SetParent(controller.transform);
    }

    // Update Function: function that is called every frame that the given MonoBehavior is enabled (once a frame upon class initialization)
    void Update()
    {
        if (bridgeActive)
        {
            bridgeTimer += Time.deltaTime;
        }
        if(element == 1)
        {
            water.Raycast();
            if (raycastFinished)
            {
                SetElement(0);
                raycastFinished = false;
            }
        }

        if (Input.GetButtonDown("Fire1") && !inMenu)
        {
            inMenu = true;
            ems.Show();
        }

        if (Input.GetButtonDown("Cancel") && !inMenu)
        {
            inMenu = true;
            pms.Pause();
        }

        if (earthOnCD)
        {
            if (swordActive)
            {
                swordActive = false;
            }
            earthAlpha.enabled = true;
            float temp = 1 - (earthCDTimer / cooldown);
            earthAlpha.fillAmount = temp;
            if (earthCDTimer >= cooldown)
            {
                earthAlpha.enabled = false;
                earthCDTimer = 0f;
                earthOnCD = false;
            }
            earthCDTimer += Time.deltaTime;
        }

        if (sword.GetCastable())
        {
            swordTimer += Time.deltaTime;
            if (swordTimer >= swordDuration)
            {
                sword.SetTerminate(true);
                swordTimer = 0f;
            }
        }

        // Temp var to store the current moveDirection's Y component (only the Y value of the Vector3)
        float yStore = moveDirection.y;

        /* 
         * Calculates the vertical and horizontal components of the movement direction; because moveDirection is a Vector-type variable,
         * it's values inherently store the direction in pos vs neg signs, the values actually indicate the speed the character is moving
         * in a given direction.
         * NOTE: vertical and horizontal are the X and Z components of the vector, meaning they do not control the up and down components
         */
        moveDirection = (Input.GetAxisRaw("Vertical") * moveSpeed * transform.forward) + (Input.GetAxisRaw("Horizontal") * moveSpeed * transform.right);

        /*
         * normalized is a property of a Vector3 that recalculates the given vector to have a magnitude of 1
         * (magnitude is a fancy vector way of saying that it is the sum of the vetors)
         * normalization means that if a character moves in a non-cardinal direction, they will not move faster than they should
         */
        moveDirection = moveDirection.normalized * moveSpeed;

        // Y value is retored from temp variable; necessary because otherwise Y would contribute to vector normalization; meaning
        // that moving in a direciton and jumping would be slower than moving in a direction normally - not what we want
        moveDirection.y = yStore;

        if (controller.isGrounded && !isDashing)
        {
            canDash = true;
        }

        if (controller.isGrounded)
        {
            if (!isDashing)
            {
                if (jumpCount > 0)
                {
                    if (!isWalking && !swordActive)
                    {
                        animator.SetInteger("State", 0);
                    }
                    else if (isWalking && !swordActive)
                    {
                        animator.SetInteger("State", 1);
                    }
                    else if (!isWalking && swordActive)
                    {
                        animator.SetInteger("State", 10);
                    }
                    else
                    {
                        animator.SetInteger("State", 11);
                    }
                }
            }
            animator.SetTrigger("NotJumping");
            moveDirection.y = 0f;
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Fire2") && element == 2 && !inMenu && !earthOnCD)
        {
            sword.StartCoroutine(sword.Swing());
        }
        else if (Input.GetButtonDown("Fire2") && element == 3 && !inMenu)
        {
            fireball.Cast(player.transform.position, player.transform.rotation);
        }
        else if (Input.GetButtonDown("Fire2") && element == 1 && !inMenu)
        {
            water.Lock();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount < 1)
            {
                animator.ResetTrigger("NotJumping");
                animator.SetTrigger("Jumping");
                moveDirection.y = jump;
                jumpCount += 1;
            }
            else if (element == 4 && jumpCount < 2 && !isDashing)
            {
                animator.ResetTrigger("Jumping");
                animator.SetTrigger("Jumping");
                moveDirection.y = doubleJump;
                jumpCount += 1;
            }
        }

        if (Input.GetButtonDown("Fire3") && !isDashing && canDash && element == 4 && !inMenu)
        {
            canDash = false;
            animator.ResetTrigger("Jumping");
            animator.SetTrigger("IsDashing");
            StartCoroutine(Dash());
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

            Vector3 walkCheck1 = controller.transform.position;
            walkCheck1.y = 0f;
            controller.Move(moveDirection * Time.deltaTime);
            Vector3 walkCheck2 = controller.transform.position;
            walkCheck2.y = 0f;
            if (walkCheck1 == walkCheck2)
            {
                isWalking = false;
                animator.ResetTrigger("IsWalking");
                animator.SetTrigger("IsNotWalking");
            }
            else if (jumpCount == 0)
            {
                isWalking = true;
                animator.ResetTrigger("IsNotWalking");
                animator.SetTrigger("IsWalking");
            }
        }
    }

    public IEnumerator Bridge(GameObject anchor1, GameObject anchor2) 
    {
        float length = Vector3.Distance(anchor1.transform.position, anchor2.transform.position);
        GameObject tempGO;
        Vector3 piecePos = anchor1.transform.position;
        Vector3 offset = new(0, 10f, 0);
        int numPieces = (int)(length / 5);
        pieces = new GameObject[numPieces];
        float size = length / numPieces;
        Vector3 sizeScale = new(0, 0, size);
        float half = size / 2f;
        piecePos = Vector3.MoveTowards(piecePos, anchor2.transform.position, -half);
        anchor1.transform.LookAt(anchor2.transform);
        for (int i = 0; i < numPieces; i++)
        {
            Debug.Log("Piece " + i);
            if ((piece1Moving && piece2Moving) && i>1) 
            {
                i--;
                yield return new WaitForEndOfFrame();
                continue;
            }
            piecePos = Vector3.MoveTowards(piecePos, anchor2.transform.position, size);
            tempGO = Instantiate(prefab, piecePos - offset, anchor1.transform.rotation);
            pieces[i] = tempGO;
            tempGO.transform.localScale += sizeScale;
            StartCoroutine(MovePiece(tempGO, piecePos));
            yield return new WaitForSeconds(0.4f);
        }
        bridgeActive = true;
        while (bridgeTimer < bridgeDuration)
        {
            yield return null;
        }
        bridgeActive = false;
        bridgeTimer = 0;
        foreach (GameObject go in pieces)
        {
            Destroy(go);
        }
        water.Reset();
        yield break;
    }

    IEnumerator MovePiece(GameObject go, Vector3 final)
    {
        if (!piece1Moving)
        {
            piece1 = go;
            piece1Moving = true;
            Vector3 start1 = piece1.transform.position;
            for (int i = 0; i < 10; i++)
            {
                Debug.Log(i);
                piece1.transform.position = Vector3.Lerp(start1, final, (piece1Time / moveTime));
                piece1Time += 0.02f;
                yield return new WaitForSeconds(0.02f);
            }
            piece1Time = 0f;
            piece1Moving = false;
        }
        else
        {
            piece2 = go;
            piece2Moving = true;
            Vector3 start2 = piece2.transform.position;
            for (int i = 0; i < 10; i++)
            {
                Debug.Log(i);
                piece2.transform.position = Vector3.Lerp(start2, final, (piece2Time / moveTime));
                piece2Time += 0.02f;
                yield return new WaitForSeconds(0.02f);
            }
            piece2Time = 0f;
            piece2Moving = false;
        }
        yield break;
    }

    public void SetEarthOnCD (bool onCD)
    {
        earthOnCD = onCD;
    }

    public void SetInMenu(bool inMenu)
    {
        this.inMenu = inMenu;
    }

    public void SetRaycastFinished(bool raycastFinished)
    {
        this.raycastFinished = raycastFinished;
    }
    public void SetElement(int element)
    {
        if (element == 2 && !earthOnCD && this.element != 2)
        {
            capColl.transform.SetPositionAndRotation(armature.transform.position, armature.transform.rotation);
            capColl.transform.SetParent(armature.transform);
            Vector3 tempSwordChange = capColl.transform.localPosition;
            tempSwordChange.z += 0.5f;
            tempSwordChange.y += 0.15f;
            capColl.transform.localPosition = tempSwordChange;
            StartCoroutine(sword.SwordStart());
            swordActive = true;
        }
        if (element != 2 && this.element == 2 && !earthOnCD && sword.GetCastable())
        {
            sword.SetTerminate(true);
        }
        this.element = element;
    }

    // Coroutine function; acts as a function that can "run in the background" allowing for other functions like Update() to execute while still keeping a earthCDTimer running
    // Note: this cannot be done with increments in the update function or similar because it varies for those with different fps; the WaitForSeconds class uses time similar to deltaTime
    private IEnumerator Dash()
    {
        isDashing = true;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        animator.ResetTrigger("IsDashing");
        yield break;
    }
}
