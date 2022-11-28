using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{

    public GameObject Enemy;
    public GameObject side;
    
    //[Header("SoundEffect")]
    //public AudioSource[] Sound;
    //private int num;

    void OnCollisionEnter2D(Collision2D col)
    {
        var coldirection = transform.InverseTransformPoint (col.transform.position);
        if (col.gameObject.tag == "Player")
        {
            if(coldirection.y > 0f){
                Debug.Log("Player killed the Enemy");

                //sound effect
                //num = Random.Range (0,4);
                //Sound[num].Play();
                side.SetActive(false);
                Destroy(Enemy, 0.05f);
            }       
        }
    }
}
