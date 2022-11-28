using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{

    [Header("StartingInfo")]
    public GameObject Epilogue;
    public float downSpeed = 1.0f; 
    public bool dropDetect = false;

    [SerializeField] private float Rgravity = 9.80665f;
    private Vector3 _velocity;

    [Header("MovingInfo")]
    public float speed = 1.5f;
    public float gravity = 1.0f;
    public float XSpeed;

    public float timer = 4.0f;
    public float MoveTimer = 0.0f;

    private Rigidbody2D rb = null;
    private bool direction = false; //false:left, true:right

    [Header("Movefield")]
    public float Xmax;
    public float Xmin;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 tmp = gameObject.transform.position;
        Xmax += tmp.x;
        Xmin += tmp.x;
    }

    //change direction every three seconds
    void FixedUpdate() 
    {
        //XSpeed = GetXSpeed();
        if(Epilogue.activeSelf == false)
        {
            if(MoveTimer < 0.0f || transform.position.x > Xmax || transform.position.x < Xmin){
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
        
            if(dropDetect == true)
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
        if (!direction) //left
        {
            XSpeed = -speed;
        }
        else //right
        {
            XSpeed = speed;
        }
        return XSpeed;
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
        //MoveTimer -= 1.0f;
        dropDetect = true;
        //Debug.Log("Droping!");
    }

}
