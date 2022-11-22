using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    private int collectibleCount;
    private int heartLeft = 3;
    private UiManager playerUi;


    private void Start()
    {
        playerUi = GameObject.Find("Canvas").GetComponent<UiManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case Globals.COLLECTIBLE_TAG:
                playerUi.setCollectibleVisible(true);
                collectibleCount++;
                playerUi.UpdateCollectibleCount(collectibleCount);
                Destroy(other.gameObject);
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.collider.CompareTag("side"))
        {
            if (heartLeft > 0)
            {
                heartLeft--;

            }
            playerUi.UpdateHeartLeft(heartLeft);
            playerUi.SetGameOverScreenVisible(heartLeft <= 0);
        }
    }


}
