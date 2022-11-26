using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverController : MonoBehaviour
{
    // Start is called before the first frame update
    public void restartLevel()
    {
        Time.timeScale = 1;
        LevelSelect.reload(this);
        PlayerMovement.gameOverFallCamera = false;
    }

    public void MainMenu()
    {
        JsonSaveDAO json = new JsonSaveDAO(Application.persistentDataPath);
        json.updateCurrentLevel(SceneManager.GetActiveScene().name);
        Debug.Log(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        LevelSelect.loadMainMenu(this);
    }
}
