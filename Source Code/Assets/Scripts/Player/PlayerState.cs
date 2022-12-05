using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] float HeartFillTotal = 3f;
    [SerializeField] bool isRelateToColorItemTotal = false;

    private int collectibleCount;
    private int colorItemCount;
    private UiManager playerUi;
    private CameraLimits cameraLimits;
    [SerializeField] AudioClip CoinSound;
    [SerializeField] AudioClip ColorItemSound;

    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] SaturationManager saturation;
    AudioSource mimiSource;

    private void Awake()
    {
        if (!isRelateToColorItemTotal)
        {
            return;
        }
        if (!GameObject.Find("ColorItem"))
        {
            HeartFillTotal = 1;
            return;
        }
        HeartFillTotal = GameObject.Find("ColorItem").GetComponentsInChildren<CircleCollider2D>().Length;
        //or ColorItemTotal = GameObject.FindGameObjectsWithTag("ColorItem").Length;


    }
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
                Destroy(other.gameObject);
                break;
            case Globals.COLOR_ITEM_TAG:
                if (colorItemCount < HeartFillTotal)
                {
                    colorItemCount++;
                    mimiSource.PlayOneShot(ColorItemSound, 0.8f);
                    playerUi.UpdateHeart(colorItemCount / HeartFillTotal);
                    saturation.IncreaseSaturation(100f / HeartFillTotal, 0.2f);
                    Destroy(other.gameObject);
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("side") || col.collider.CompareTag("ivy"))
        {
            if (colorItemCount >= 0)
            {
                mimiSource.PlayOneShot(hitSound);
                colorItemCount--;
            }
            else if (colorItemCount < 0)
            {
                mimiSource.PlayOneShot(deathSound);
            }
            saturation.DecreaseSaturation(100f / HeartFillTotal, 0.2f);
            playerUi.UpdateHeart(colorItemCount / HeartFillTotal);
            playerUi.SetGameOverScreenVisible(colorItemCount < 0);
        }
    }

    void LeapOfFaith() // Kills Mimi when she falls out of bounds
    {
        if (transform.position.y < cameraLimits.LeapOfFaithValue())
        {
            colorItemCount = 0;
            saturation.DecreaseSaturation((float)(HeartFillTotal - colorItemCount) / (float)HeartFillTotal * 100f, 0.2f);
            mimiSource.PlayOneShot(deathSound);
            playerUi.UpdateHeart((float)colorItemCount / (float)HeartFillTotal);
            playerUi.SetGameOverScreenVisible(colorItemCount <= 0);
            transform.position = new Vector3(-9.48f, -3.631f, transform.position.z);
            cameraLimits.freezeCamera();
            CameraLimits.gameOverFallCamera = true;
        }
    }

    public int getcollectibleCount()
    {
        return collectibleCount;
    }
}



