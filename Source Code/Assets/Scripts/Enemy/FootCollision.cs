using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollision : MonoBehaviour
{
    public static FootCollision instance;
    public float DropDetectCnt = 0.0f;
    public bool dropDetect = false;
    // Start is called before the first frame update

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
            if (Mathf.Abs(col.contacts[0].normal[0]) < 0.72f)
            {
                DropDetectCnt = 0.5f;
                dropDetect = false;
            }
            else
            {
                dropDetect = true;
            }
        }
    }

    /*
    void OnCollisionExit2D (Collision2D col)
    {

        Debug.Log("down");
        //DropDetectCnt -= Time.deltaTime;
        if(DropDetectCnt <=  0.0f)
        {
            dropDetect = true;
        }  
    }
    */

}
