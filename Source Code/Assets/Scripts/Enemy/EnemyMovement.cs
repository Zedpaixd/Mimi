using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    [Header("MovingInfo")]
    public float speed = Globals.ENEMY_SPEED;
    public float cliffThreshold = 0.5f;
    float distanceCrossed;
    [SerializeField] float distanceToCross = 2f;
    //change direction every three seconds
    private void Update()
    {
        Vector3 direction = Vector3.right * Time.deltaTime;
        distanceCrossed += Time.deltaTime * Mathf.Abs(speed);
          if (distanceCrossed >= distanceToCross)
          {
              changeDirection();
          }
        transform.Translate(direction * speed);
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.right * speed).normalized * 0.5f, Vector2.down);
        if ((hit.collider == null && hit.distance <= cliffThreshold))
        {
            changeDirection();

        }
    }
    void changeDirection()
    {
        speed = -speed;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        distanceCrossed = 0;

    }
    //setting the direction for the enemy movement
    //enemy's direction will change to another way if enemy hit the player

    private void OnTriggerEnter2D(Collider2D col) {

        if (col.tag == "Player" || col.tag == "Ground" || col.tag == "enemy")
        {
            changeDirection();
        }
    }
}

