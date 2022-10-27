using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using DragonBones;

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
    bool canMove = true;

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
    /*
     * Animator animator;
     * SpriteRenderer spriteRenderer;
     */
    Rigidbody2D body;
    private UnityArmatureComponent armatureComponent;
    private bool facingRight;



    private void Awake()
    {
        instance = this;
    }

    #endregion

    /*private void OnPreRender()
    {
        canMove = true;
    }*/

    private void Start()
    {
        jumpCounter = 0;
        //        animator = GetComponent<Animator>();
        //        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        collectibleUI = GameObject.Find("CollectibleCanvas").GetComponent<UiManager>();

        gravity = -(2 * maxJumpHeight) / timeToJumpApex;
        jumpForce = (Mathf.Abs(gravity) * timeToJumpApex);

        _layerMask = LayerMask.GetMask(Globals.OBJECT_LAYER);

        armatureComponent = GetComponent<UnityArmatureComponent>();


        /*                                                                              // Maybe for some
        maxJumpVelocity = (Mathf.Abs(gravity) * timeToJumpApex)/2;                     // other time :)
        minJumpVelocity = (Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight))/2;
        */

    }


    void Update()
    {
        moveSpeed = Mathf.MoveTowards(moveSpeed, maxMoveSpeed, Time.deltaTime * Globals.DELTA_SMOOTHENING);
        GetInput();

        animateCharacter();
        WallCheck();

        if (canMove)
            ApplyMovement();

        Debug.DrawRay(transform.position - new Vector3(0, 0.2f, 0), Vector2.right, Color.green);
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), Vector2.right, Color.green);
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

        flipPlayer(armatureComponent);
    }

    private void flipPlayer(UnityArmatureComponent armatureComponent)
    {
        if ((direction < 0 && !facingRight) || (direction > 0 && facingRight))
        {
            facingRight = !facingRight;
            armatureComponent.armature.flipX = !armatureComponent.armature.flipX;
        }
    }

    private void WallCheck()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position - new Vector3(0, 0.2f, 0), Vector2.right, Globals.RAYCAST_CHECK_RANGE, _layerMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(0, 0.2f, 0), Vector2.left, Globals.RAYCAST_CHECK_RANGE, _layerMask);
        RaycastHit2D hitRightUp = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector2.right, Globals.RAYCAST_CHECK_RANGE, _layerMask);
        RaycastHit2D hitLeftUp = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector2.left, Globals.RAYCAST_CHECK_RANGE, _layerMask);
        if (((hitRight.collider != null || hitRightUp.collider != null) && direction > 0) || ((hitLeft.collider != null || hitLeftUp.collider != null) && direction < 0))  //this is ugly code; better way?
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
        }
        transform.Translate(Vector3.right * direction * Time.deltaTime * moveSpeed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(Globals.GROUND_TAG))
        {
            jumpCounter = body.velocity.y < 0.15f ? 0 : 1;
            isOnGround = true;
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
