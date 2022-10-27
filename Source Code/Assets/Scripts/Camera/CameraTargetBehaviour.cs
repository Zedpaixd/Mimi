using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetBehaviour : MonoBehaviour
{

    public float distance = 4;

    private PlayerMovement player;

    void Start()
    {
        player = PlayerMovement.instance;
    }
    Vector3 localPos;
    private void lateUpdate()
    {


        if (player.GetDirection() != 0)
        {

            transform.localPosition = localPos * distance;
        }
    }
    private void Update()
    {
        localPos = new Vector3(
     player.GetDirection() < 0 ? -1f : 1.6f,
     0f,
     0f
 );
    }

}
