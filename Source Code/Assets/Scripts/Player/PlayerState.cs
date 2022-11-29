using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private int collectibleCount;
    private int heartLeft = 3;
    private UiManager playerUi;
    private CameraLimits cameraLimits;
    [SerializeField] AudioClip CoinSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] SaturationManager saturation;
    AudioSource mimiSource;

    private void Start()
    {
        mimiSource = GetComponent<AudioSource>();
        playerUi = GameObject.Find("Canvas").GetComponent<UiManager>();
        cameraLimits = GameObject.Find("Virtual Camera").GetComponent<CameraLimits>();
    }

    private void Update()
    {
        LeapOfFaith();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case Globals.COLLECTIBLE_TAG:
                mimiSource.PlayOneShot(CoinSound, 0.7f);
                collectibleCount++;
                playerUi.setCollectibleVisible(collectibleCount > 0);
                playerUi.UpdateCollectibleCount(collectibleCount);
                saturation.IncreaseSaturation(1f, 0.2f);
                Destroy(other.gameObject);
                break;

            case Globals.END_TAG:
                playerUi.SetEndScreenVisible();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.collider.CompareTag("side"))
        {
            if (heartLeft > 0)
            {
                mimiSource.PlayOneShot(hitSound);
                heartLeft--;
            }
            else if (heartLeft == 0)
            {
                mimiSource.PlayOneShot(deathSound);
            }
            saturation.DecreaseSaturation(100f / 3f, 0.2f);
            playerUi.UpdateHeartLeft(heartLeft);
            playerUi.SetGameOverScreenVisible(heartLeft <= 0);
        }
    }

    void LeapOfFaith()   // ??? use descriptive function names or add comments to explain the code   -Armand.
    {
        if (transform.position.y < cameraLimits.LeapOfFaithValue())
        {
            heartLeft = 0;
            mimiSource.PlayOneShot(deathSound);
            playerUi.UpdateHeartLeft(heartLeft);
            playerUi.SetGameOverScreenVisible(heartLeft <= 0);

            transform.position = new Vector3(-9.48f, -3.631f, transform.position.z);
            cameraLimits.freezeCamera();
            CameraLimits.gameOverFallCamera = true;
        }
    }
}



