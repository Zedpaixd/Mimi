using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Cinemachine;
using System.Diagnostics.Tracing;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    #region var definitions

    [Header("Movement")]
    [SerializeField] private float maxMoveSpeed = 5.5f;
    //[SerializeField] private float minMoveSpeed = 0.5f;
    [SerializeField] private float gravity;
    float direction;
    float moveSpeed = 5.5f;
    int _layerMask;
    public bool canMove = true;
    private Vector2 forceDirection;
    private float tmpDirect;

    [Header("Jumping")]
    [SerializeField] private float maxJumpHeight = 2.5f;
    //[SerializeField] private float minJumpHeight = 4f;
    [SerializeField] private float timeToJumpApex = .6f;
    [SerializeField] private int maxJumps = 1;
    float jumpForce;
    int jumpCounter;
    private bool isOnGround = true;

    [Header("Collectible")]

    private int collectibleCount;
    private UiManager collectibleUI;

    [Header("Player")]

    public static PlayerMovement instance;
    Animator animator;
    /* SpriteRenderer spriteRenderer;
    *  private UnityArmatureComponent armatureComponent;
    */
    Rigidbody2D body;
    private bool facingRight = true;
    [SerializeField] PauseController Pause;

    [Header("Action")]
    [SerializeField] private bool isCrouching;

    [Header("Secret Areas")]
    private bool secretAreaTest = false;
    public GameObject Fitting;
    public GameObject Fitting2;
    public GameObject Fitting3;
    public GameObject HiddenCollectable1;
    public GameObject HiddenCollectable2;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CameraLimits cameraLimits;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    private void Start()
    {
        jumpCounter = 0;
        animator = GetComponent<Animator>();
        //        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        collectibleUI = GameObject.Find("Canvas").GetComponent<UiManager>();
        gravity = -(2 * maxJumpHeight) / timeToJumpApex;
        jumpForce = (Mathf.Abs(gravity) * timeToJumpApex);
        _layerMask = LayerMask.GetMask(Globals.OBJECT_LAYER);
        //   armatureComponent = GetComponent<UnityArmatureComponent>();

        cameraLimits = cinemachineVirtualCamera.GetComponent<CameraLimits>();
    }


    void Update()
    {
        moveSpeed = Mathf.MoveTowards(moveSpeed, maxMoveSpeed, Time.deltaTime * Globals.DELTA_SMOOTHENING);
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        //Debug.Log(rb.velocity.magnitude);
        cameraLimits.SmartCameraFollowingThePlayer(transform.position.x, transform.position.y);

        if (canMove)
        {
            GetInput();
            animateCharacter();
            jump();
            WallCheck();
            DisableMovement();
        }
        Debug.DrawRay(transform.position + new Vector3(0, 0.05f, 0), Vector2.right, Color.green);
        Debug.DrawRay(transform.position + new Vector3(0, 0.8f, 0), Vector2.right, Color.green);
        escapeSetting();

        //Debug.Log(SceneManager.GetSceneByBuildIndex(2).name);//SceneManager.GetActiveScene().buildIndex + 1);//SceneManager.GetSceneByBuildIndex(0).name);

    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            ApplyMovement();
        }
    }

    void escapeSetting()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause.isVisible(!Pause.gameObject.activeSelf);
            Time.timeScale = Convert.ToInt16(!Pause.gameObject.activeSelf);
        }
    }

    private void GetInput()
    {
        direction = Input.GetAxis("Horizontal");
    }

    public float GetDirection()
    {
        if (direction != 0)
        {
            tmpDirect = direction;
        }
        return direction;
    }

    private void animateCharacter()
    {
        animator.SetFloat("Speed", moveSpeed);
        animator.SetFloat("Direction", Mathf.Abs(direction));
        flipPlayer();
    }

    private void flipPlayer()
    {
        if ((direction < 0 && facingRight) || (direction > 0 && !facingRight))
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void WallCheck()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(0, 0.05f, 0), Vector2.right, Globals.RAYCAST_CHECK_RANGE, _layerMask);   // this is
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + new Vector3(0, 0.05f, 0), Vector2.left, Globals.RAYCAST_CHECK_RANGE, _layerMask);     // very ugly
        RaycastHit2D hitRightUp = Physics2D.Raycast(transform.position + new Vector3(0, 0.8f, 0), Vector2.right, Globals.RAYCAST_CHECK_RANGE, _layerMask); // code. Any
        RaycastHit2D hitLeftUp = Physics2D.Raycast(transform.position + new Vector3(0, 0.8f, 0), Vector2.left, Globals.RAYCAST_CHECK_RANGE, _layerMask);   // better ideas?
        if (((hitRight.collider != null || hitRightUp.collider != null) && direction > 0) || ((hitLeft.collider != null || hitLeftUp.collider != null) && direction < 0))
        {
            direction = 0;
        }
    }
    private void ApplyMovement()
    {
        Debug.DrawRay(transform.position, body.velocity);
        //    body.MovePosition(transform.position + Vector3.right * direction * Time.deltaTime * moveSpeed);
        transform.Translate(Vector3.right * direction * Time.deltaTime * moveSpeed);
        // body.velocity = new Vector2(direction * moveSpeed, body.velocity.y);
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && jumpCounter < maxJumps)  // why was it isonground || jC<mJ?
        {
            //add double jump with jumpCounter
            isOnGround = false;
            jumpCounter++;
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.Play("Mimi_jump");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            isCrouching = true;
            animator.SetBool("isCrouching", isCrouching);
            animator.Play("Mimi_crouch");
        }

    }

    private void DisableMovement()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.W))
        {
            isCrouching = false;
            animator.SetBool("isCrouching", isCrouching);
        }
    }

    // Trigger to not create collision bug with collectible
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(Globals.GROUND_TAG))
        {
            if (Math.Abs(col.contacts[0].normal[0]) < 0.72f && Math.Abs(body.velocity.y) < 0.02f) //|| Math.Abs(body.velocity.y) < 0.15f)
            {
                jumpCounter = 0;
                isOnGround = true;

            }
            else
            {
                jumpCounter = 1;
                isOnGround = false;

            }
            animator.Play("Mimi_land");
        }

        var coldirection = transform.InverseTransformPoint(col.transform.position);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(Globals.GROUND_TAG))
        {
            if (Math.Abs(col.contacts[0].normal[0]) < 0.72f && body.velocity.y < 1.1f) // HORRIBLE fix but it works.
            {
                jumpCounter = 0;
                isOnGround = true;
            }
            else
            {
                jumpCounter = 1;
                isOnGround = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject secretArea = other.gameObject;
        if (secretArea.CompareTag(Globals.SECRET_AREA_TAG))
        {
            if (secretArea.name == "SecretAreaWall" && !secretArea)
            {
                secretAreaTest = true;
                FadeOut(secretArea);
                Fitting.SetActive(true);
                HiddenCollectable1.SetActive(true);

            }
            else if (secretArea.name == "SecretAreaWall (1)" && !secretAreaTest)
            {
                secretAreaTest = true;
                FadeOut(secretArea);
                Fitting.SetActive(false);
                Fitting2.SetActive(false);
                Fitting3.SetActive(true);
                HiddenCollectable2.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject secretArea = other.gameObject;
        if (secretArea.CompareTag(Globals.SECRET_AREA_TAG))
        {
            Debug.Log("You left the secret area.");
            if (secretArea.name == "SecretAreaWall" && secretAreaTest)
            {
                secretAreaTest = false;
                FadeIn(secretArea);
            }
            else if (secretArea.name == "SecretAreaWall (1)" && secretAreaTest)
            {
                secretAreaTest = false;
                FadeIn(secretArea);
            }
        }
    }

    IEnumerator FadeOutCoroutine(GameObject secretArea)
    {
        TilemapRenderer rend = secretArea.GetComponent<TilemapRenderer>();
        for (float i = 1; i > 0.5f; i -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = i;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void FadeOut(GameObject secretArea)
    {
        IEnumerator coroutine = FadeOutCoroutine(secretArea);
        StartCoroutine(coroutine);
    }

    IEnumerator FadeInCoroutine(GameObject secretArea)
    {
        TilemapRenderer rend = secretArea.GetComponent<TilemapRenderer>();
        Color c = rend.material.color;
        for (float i = 0.5f; i < 1.05; i += 0.05f)
        {
            c.a = i;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void FadeIn(GameObject secretArea)
    {
        IEnumerator coroutine = FadeInCoroutine(secretArea);
        StartCoroutine(coroutine);
    }

    // Attack and hit movement


    // Hit movement

}
