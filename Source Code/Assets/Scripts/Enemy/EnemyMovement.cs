using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{

    [Header("StartingInfo")]
    public GameObject Epilogue;
    public float downSpeed = Globals.ENEMY_DOWN_SPEED;
    public bool dropDetect = false;
    public float DropDetectCnt = 3.0f;

    private Vector3 _velocity;

    [Header("MovingInfo")]
    public float speed = Globals.ENEMY_SPEED;
    public float XSpeed;

    public float timer = Globals.ENEMY_TIMER;
    public float MoveTimer = Globals.ENEMY_MOVE_TIMER;
    bool raycast;

    private Rigidbody2D rb = null;
    private bool direction = false; //false:left, true:right
    // ^ can just use an int that is either -1 or 1 then say speed = speed * direction; no need for ifs.

    [Header("Movefield")]
    public Vector3 tmp;
    public float Xmax;
    public float Xmin;
    [SerializeField] public Transform feet;
    public float cliffThreshold = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tmp = gameObject.transform.position;
        Xmax += tmp.x;
        Xmin += tmp.x;
    }

    //change direction every three seconds
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.right * speed).normalized*0.5f, Vector2.down);
        Vector3 direction = Vector3.right * Time.deltaTime;
        Debug.DrawRay(transform.position + (Vector3.right * speed).normalized, Vector2.down);
        if (Time.frameCount % 400 == 0 || (hit.collider == null && hit.distance <= cliffThreshold))
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            speed = -speed;
        }

        transform.Translate(direction * speed);
    }


    //setting the direction for the enemy movement
    //enemy's direction will change to another way if enemy hit the player
      void OnCollisionEnter2D(Collision2D col)
      {
          //Debug.Log("OnGround!");
          if (col.collider.tag == "Player" || col.collider.tag == "Ground"||col.collider.tag == "Enemy")
          {
              speed = -speed;
              transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
          }

      }

}

