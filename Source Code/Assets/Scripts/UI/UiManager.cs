using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    [SerializeField] private Text collectibleCountText;
    [SerializeField] private GameObject Collectible;
    [SerializeField] private GameObject Life;
    [SerializeField] List<Image> Heart;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject endScreen;

    public void setLifeVisible(bool a)
    {
        if (Life.activeSelf != a)
            Life.SetActive(a);

    }
    public void setCollectibleVisible(bool a)
    {
        if (Collectible.activeSelf != a)
            Collectible.SetActive(a);

    }
    public void UpdateCollectibleCount(int collectibleCount)
    {
        if (collectibleCount > 1)
            collectibleCountText.text = collectibleCount.ToString();
    }

    public void UpdateHeartLeft(int heartNumber)
    {
        foreach (Image heart in Heart)
        {
            heart.gameObject.SetActive(heartNumber > 0);
            heartNumber--;
        }
    }
    public void SetGameOverScreenVisible(bool a)
    {
        Time.timeScale = a ? 0 : 1;
        gameOverScreen.SetActive(a);
    }

    public void SetEndScreenVisible()
    {
        Time.timeScale = 0;
        endScreen.SetActive(true);
    }
}
