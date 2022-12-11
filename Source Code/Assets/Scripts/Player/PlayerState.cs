using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    [SerializeField] float hitDelay = 0.3f;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] SaturationManager saturation;
    [SerializeField] float repulsePlayerAttack = 1.6f;

    AudioSource mimiSource;
    float xSpawnPos;
    float ySpawnPos;
    bool isHitable = true;

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
        xSpawnPos = transform.position.x;
        ySpawnPos = transform.position.y;
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
        if ((col.collider.CompareTag("enemy") || col.collider.CompareTag("ivy")) && isHitable)
        {
            StartCoroutine(hitWithDelay(hitDelay));
            StartCoroutine(OpacityFlickering(hitDelay / 4f));
            saturation.DecreaseSaturation(100f / HeartFillTotal, 0.2f);
            playerUi.UpdateHeart(colorItemCount / HeartFillTotal);
            playerUi.SetGameOverScreenVisible(colorItemCount < 0);
        }
        else
        if (col.collider.CompareTag("head") && isHitable)
        {
            AttackJump();
        }
    }

    IEnumerator OpacityFlickering(float frequency)
    {
        while (!isHitable)
        {
            foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>(true))
            {
                Color color = sprite.material.color;
                color.a = color.a == 0.1f ? 1f : 0.1f;
                sprite.material.color = color;
            }

            yield return new WaitForSeconds(frequency);
        }

        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>(true))
        {
            Color color = sprite.material.color;
            color.a = 1f;
            sprite.material.color = color;
        }
    }

    void AttackJump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * repulsePlayerAttack, ForceMode2D.Impulse);
        mimiSource.PlayOneShot(hitSound);
    }

    IEnumerator hitWithDelay(float delay)
    {
        HitJump();
        if (colorItemCount >= 0)
        {
            mimiSource.PlayOneShot(hitSound);
            colorItemCount--;
            isHitable = false;
            yield return new WaitForSeconds(delay);
            isHitable = true;
        }
        else if (colorItemCount < 0)
        {
            mimiSource.PlayOneShot(deathSound);
        }
    }
    void HitJump()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        int faceLeftRight = transform.localScale.x > 0 ? 1 : -1;
        rb.AddForce((-faceLeftRight * Vector2.right).normalized * repulsePlayerAttack, ForceMode2D.Impulse);
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
            transform.position = new Vector3(xSpawnPos, ySpawnPos, transform.position.z);
            cameraLimits.freezeCamera();
            CameraLimits.gameOverFallCamera = true;
        }
    }

    public int getcollectibleCount()
    {
        return collectibleCount;
    }
}



