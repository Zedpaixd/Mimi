using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Text collectibleCountText;
    [SerializeField] private GameObject Collectible;
    [SerializeField] private Image Life;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject endScreen;
    [SerializeField] int totalNbCoins;
    [SerializeField] VisualEffect fireworks;

    private void Start()
    {
        Life.fillAmount = 0;
    }
    public void setCollectibleVisible(bool a)
    {
        Collectible.SetActive(a);
    }
    public void UpdateCollectibleCount(int collectibleCount)
    {
        if (collectibleCount > 1)
            collectibleCountText.text = collectibleCount.ToString();
    }

    public void UpdateHeart(float fillAmount)
    {
        if (fillAmount > Life.fillAmount)
        {
            StartCoroutine(increaseHearth(fillAmount - Life.fillAmount));
        }
        else
        {
            StartCoroutine(decreaseHearth(Life.fillAmount - fillAmount));
        }
    }

    public void SetGameOverScreenVisible(bool a)
    {
        Time.timeScale = a ? 0 : 1;
        gameOverScreen.SetActive(a);
    }

    public void SetEndScreenVisible(int nbCoins)
    {
        float completionPercentage = (nbCoins * 100) / totalNbCoins;
        string result = "Collected coins: " + nbCoins + "/" + totalNbCoins + " | " + completionPercentage + "%";
        endScreen.transform.GetChild(1).GetComponent<Text>().text = result;
        Time.timeScale = 0;
        endScreen.SetActive(true);
    }

    IEnumerator increaseHearth(float fillAmount)
    {
        float beginFillAmount = Life.fillAmount;
        while (Life.fillAmount < fillAmount + beginFillAmount)
        {
            Life.fillAmount += fillAmount * Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(fillAmount * Time.unscaledDeltaTime);
        }
    }

    IEnumerator decreaseHearth(float fillAmount)
    {
        float beginFillAmount = Life.fillAmount;

        while (Life.fillAmount >= beginFillAmount - fillAmount)
        {
            Life.fillAmount -= fillAmount * Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(fillAmount * Time.unscaledDeltaTime);
        }

    }
}
