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
    SpriteRenderer spriteRenderer;
    Rigidbody2D body;


    private void Awake()
    {
        instance = this;
    }

    #endregion

    private void Start()
    {
        jumpCounter = 0;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        collectibleUI = GameObject.Find("CollectibleCanvas").GetComponent<UiManager>();

        gravity = -(2 * maxJumpHeight) / timeToJumpApex;
        jumpForce = (Mathf.Abs(gravity) * timeToJumpApex);

        /*                                                                              // Maybe for some
        maxJumpVelocity = (Mathf.Abs(gravity) * timeToJumpApex)/2;                     // other time :)
        minJumpVelocity = (Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight))/2;
        */
    }

    void Update()
    {
        moveSpeed = Mathf.MoveTowards(moveSpeed, maxMoveSpeed, Time.deltaTime * Globals.DELTA_SMOOTHENING);
        GetInput();
        if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || jumpCounter < maxJumps))
        {
            //add double jump with jumpCounter
            jumpCounter++;
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }
        animateCharacter();
        ApplyMovement();
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
        //spriteRenderer.sprite = direction != 0 ? walkingSprite : standingSprite;
        spriteRenderer.flipX = direction < 0 ? true : false;
    }

    private void ApplyMovement()
    {
        transform.Translate(Vector3.right * direction * Time.deltaTime * moveSpeed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(Globals.GROUND_TAG) && col.GetContact(0).normal.Equals(Vector3.up))
        {
            jumpCounter = 0;
            isOnGround = true;
        }
    }
    //Trigger to not create collision bug with collectible
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
