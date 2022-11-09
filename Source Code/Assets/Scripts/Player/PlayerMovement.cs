using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    #region var definitions

    [Header("Movement")]
    [SerializeField] private float maxMoveSpeed = 5.5f;
    [SerializeField] private float minMoveSpeed = 0.5f;
    [SerializeField] private float gravity;
    float direction;
    float moveSpeed = 5.5f;
    int _layerMask;
    public bool canMove = true;

    [Header("Jumping")]
    [SerializeField] private float maxJumpHeight = 2.5f;
    [SerializeField] private float minJumpHeight = 4f;
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


        /*                                                                              // Maybe for some
        maxJumpVelocity = (Mathf.Abs(gravity) * timeToJumpApex)/2;                     // other time :)
        minJumpVelocity = (Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight))/2;
        */

    }


    void Update()
    {
        moveSpeed = Mathf.MoveTowards(moveSpeed, maxMoveSpeed, Time.deltaTime * Globals.DELTA_SMOOTHENING);


        if (canMove)
        {
            GetInput();
            animateCharacter();
            WallCheck();
            ApplyMovement();
            DisableMovement();
        }
        Debug.DrawRay(transform.position + new Vector3(0, 0.05f, 0), Vector2.right, Color.green);
        Debug.DrawRay(transform.position + new Vector3(0, 0.8f, 0), Vector2.right, Color.green);
        escapeSetting();
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
        return direction;
    }
    private void animateCharacter()
    {
        /*
         * spriteRenderer.sprite = direction != 0 ? walkingSprite : standingSprite;
         *      spriteRenderer.flipX = direction < 0 ? true : false;
         */

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

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && jumpCounter < maxJumps)  // why was it isonground || jC<mJ?
        {
            //add double jump with jumpCounter
            jumpCounter++;
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
            animator.Play("Mimi_jump");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W))
        {
            isCrouching = true;
            animator.SetBool("isCrouching", isCrouching);
            animator.Play("Mimi_crouch");
        }
        animator.SetFloat("Speed", moveSpeed);
        animator.SetFloat("Direction", Mathf.Abs(direction));
        transform.Translate(Vector3.right * direction * Time.deltaTime * moveSpeed);
    }

    private void DisableMovement()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.W))
        {
            isCrouching = false;
            animator.SetBool("isCrouching", isCrouching);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(Globals.GROUND_TAG))
        {
            jumpCounter = body.velocity.y < 0.15f ? 0 : 1;   // this is bad, any better ideas?
            isOnGround = true;
            animator.Play("Mimi_land");
        }
    }
    //trigger to not create collision bug with collectible
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Globals.COLLECTIBLE_TAG))
        {
            Destroy(other.gameObject);
            collectibleCount++;
            collectibleUI.UpdateCollectible(collectibleCount);

        }
        if (other.gameObject.CompareTag(Globals.SECRET_AREA_TAG))
        {
            Destroy(other.gameObject);
        }
    }


}
