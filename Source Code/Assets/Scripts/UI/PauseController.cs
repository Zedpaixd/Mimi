using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update
    public void isVisible(bool visible)
    {
        gameObject.SetActive(visible);

    }

    public void MainMenuButton()
    {
        JsonSaveDAO json = new JsonSaveDAO(Application.persistentDataPath);
        json.updateCurrentLevel(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        LevelSelect.loadMainMenu(this);
    }
    public void resume()
    {
        Time.timeScale = 1;
        isVisible(false);
    }
}
