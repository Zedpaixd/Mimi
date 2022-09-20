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
    int direction;
    float moveSpeed = 5.5f;

    [Header("Jumping")]
    [SerializeField] private float maxJumpHeight = 2.5f;
    [SerializeField] private float minJumpHeight = 4f;
    [SerializeField] private float timeToJumpApex = .6f;
    [SerializeField] private int maxJumps = 1;
    float jumpForce;
    int jumpCounter;
    

    //float maxJumpVelocity;
    //float minJumpVelocity;

    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D body;

    

    #endregion


    private void Start()
    {
        jumpCounter = 0;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();

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


        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < maxJumps)
        {
            if (jumpCounter == 0)
            {
                moveSpeed /= Globals.SLOW_ON_JUMP;

            }
            jumpCounter++;
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            

        }

        ApplyMovement();
    }

    private void GetInput()
    {

        direction = Input.GetKeyDown(KeyCode.D) ? 1 : Input.GetKeyDown(KeyCode.A) ? -1 : direction;

        if ((Input.GetKeyUp(KeyCode.D) && direction == 1) || (Input.GetKeyUp(KeyCode.A) && direction == -1)) direction = 0;


    }

    private void ApplyMovement()
    {

        spriteRenderer.flipX = direction < 0 ? true : false;

        transform.Translate((Vector3.right* direction * Time.deltaTime)  *  moveSpeed);
        
    }

    void OnCollisionEnter2D(Collision2D Col)
    {
        if (Col.gameObject.tag == Globals.GROUND_TAG)
        {
            jumpCounter = 0;
        }
    }

}
