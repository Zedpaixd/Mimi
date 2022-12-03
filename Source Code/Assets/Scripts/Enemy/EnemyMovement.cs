using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{

    [Header("StartingInfo")]
    public GameObject Epilogue;
    public float downSpeed = Globals.ENEMY_DOWN_SPEED; 
    public bool dropDetect = false;

    [SerializeField] private float Rgravity = Globals.ENEMY_RGRAVITY;
    private Vector3 _velocity;

    [Header("MovingInfo")]
    public float speed = Globals.ENEMY_SPEED;
    public float gravity = Globals.ENEMY_GRAVITY;   // why define double gravity?
    public float XSpeed;

    public float timer = Globals.ENEMY_TIMER;
    public float MoveTimer = Globals.ENEMY_MOVE_TIMER;

    private Rigidbody2D rb = null;
    private bool direction = false; //false:left, true:right
    // ^ can just use an int that is either -1 or 1 then say speed = speed * direction; no need for ifs.

    [Header("Movefield")]
    public Vector3 tmp;
    public float Xmax;
    public float Xmin;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tmp = gameObject.transform.position;
        Xmax += tmp.x;
        Xmin += tmp.x;
    }

    //change direction every three seconds
    void FixedUpdate() 
    {
        //XSpeed = GetXSpeed();
        if(Epilogue.activeSelf == false)
        {
            if(!dropDetect)
            {
                if(MoveTimer < 0.0f){
                    if(direction){
                        direction = false;
                    }
                    else
                    {
                        direction = true;
                    }
                    XSpeed = GetXSpeed();
                    MoveTimer = timer;
                    //Debug.Log(MoveTimer);
                }
                else{
                    MoveTimer -= Time.deltaTime;
                    XSpeed = GetXSpeed();
                    //Debug.Log(MoveTimer);
                } 

                if(transform.position.x >= Xmax)  
                {
                    if(direction){
                        direction = false;
                    }
                    else
                    {
                        direction = true;
                    }
                    transform.position = new Vector2(Xmax-0.01f, tmp.y);                
                    XSpeed = GetXSpeed();
                    MoveTimer = timer;
                    Debug.Log("max");
                }   

                if(transform.position.x <= Xmin)
                {
                    if(direction){
                        direction = false;
                    }
                    else
                    {
                        direction = true;
                    }
                    transform.position = new Vector2(Xmin+0.01f, tmp.y);
                    XSpeed = GetXSpeed();
                    MoveTimer = timer;
                    Debug.Log("min");
                }               
            }
            else
            {
                //Debug.Log("DropDetection");
                _velocity.y += -Rgravity * Time.fixedDeltaTime;

                var p = transform.position;

                p += _velocity * Time.fixedDeltaTime;
                transform.position = p;

            }

            rb.velocity = new Vector2(XSpeed,-gravity);

        }

    }

    //setting the direction for the enemy movement
    private float GetXSpeed()
    {
        return direction ? speed : -speed;
    }

    //enemy's direction will change to another way if enemy hit the player
    void OnCollisionEnter2D(Collision2D col)
    {
        dropDetect = false;
        //Debug.Log("OnGround!");

        var coldirection = transform.InverseTransformPoint (col.transform.position);
        if (col.collider.tag == "Player" && col.collider.tag == "Ground")
        {
            if(coldirection.x > 0f){
                MoveTimer -= 1.0f;
                direction = false;
                //Debug.Log("Hitted right side");
            }
            else{
                MoveTimer -= 1.0f;
                direction = true;

                //Debug.Log("Hitted left side");
            }
        }
    }

    void OnCollisionStay2D (Collision2D col)
    {
        dropDetect = false;
       //Debug.Log("OnGround!");
    }

    void OnCollisionExit2D (Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
        //MoveTimer -= 1.0f;
        dropDetect = true;
        Debug.Log("Droping!");
        }
    }

}
