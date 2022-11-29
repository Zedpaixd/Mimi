using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WaitingScreensController : MonoBehaviour
{
    public void RestartLevel()
    {
        Time.timeScale = 1;
        LevelSelect.reload(this);
        CameraLimits.gameOverFallCamera = false;
    }

    public void NextLevel()
    {
        JsonSaveDAO json = new JsonSaveDAO(Application.persistentDataPath);
        json.updateCurrentLevel("Level 2");
        Time.timeScale = 1;
        LevelSelect.StartLevel("Level 2", this);
        CameraLimits.gameOverFallCamera = false;
    }

    public void MainMenu()
    {
        JsonSaveDAO json = new JsonSaveDAO(Application.persistentDataPath);
        json.updateCurrentLevel(SceneManager.GetActiveScene().name);
        Debug.Log(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        LevelSelect.loadMainMenu(this);
        CameraLimits.gameOverFallCamera = false;
    }
}
