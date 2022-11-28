using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimits : MonoBehaviour
{
    // Attributes
    public float minXLimit;
    public float maxXLimit;
    public float minYLimit;
    public float maxYLimit;
    public static bool gameOverFallCamera = false;
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Handles the camera position if the players goes too close to the level's limit
    public void SmartCameraFollowingThePlayer(float x, float y)
    {
        if (!gameOverFallCamera)
        {
            //Debug.Log("x = " + x + " / y = " + y);
            if (x < minXLimit)
            {
                x = minXLimit;
            }
            if (y < minYLimit)
            {
                y = minYLimit;
            }
            if (x > maxXLimit)
            {
                x = maxXLimit;
            }
            if (y >= maxYLimit)
            {
                y = maxYLimit;
            }
        }
        else
        {
            x = minXLimit;
            y = minYLimit;
        }

        cinemachineVirtualCamera.ForceCameraPosition(new Vector3(x, y, cinemachineVirtualCamera.transform.position.z), cinemachineVirtualCamera.transform.rotation);
    }

    // Gives the correct y value to see Mimi fall off-screen during the leap of faith
    public float LeapOfFaithValue()
    {
        return minYLimit - 7;
    }

    // Freeze the camera in time when the player dies by doing a leap of faith
    public void freezeCamera()
    {
        minXLimit = transform.position.x;
        minYLimit = transform.position.y;
    }
}
