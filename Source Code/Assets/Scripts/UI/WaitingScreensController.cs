using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WaitingScreensController : MonoBehaviour
{
    List<string> levels; // SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).name returns null for some reason

    public void Start()
    {
        levels = new List<string>();
        levels.Add("Main Menu");
        levels.Add("Level 1");
        levels.Add("Level 2");
        levels.Add("Credits");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        LevelSelect.reload(this);
        CameraLimits.gameOverFallCamera = false;
    }

    public void NextLevel()
    {
        JsonSaveDAO json = new JsonSaveDAO(Application.persistentDataPath);

        if (levels[SceneManager.GetActiveScene().buildIndex + 1] != "Credits")
            json.updateCurrentLevel(levels[SceneManager.GetActiveScene().buildIndex + 1]);
        else
            json.updateCurrentLevel("Level 1");
        
        
        Time.timeScale = 1;
        LevelSelect.StartLevel(levels[SceneManager.GetActiveScene().buildIndex + 1], this);
        CameraLimits.gameOverFallCamera = false;
    }

    public void MainMenu()
    {
        JsonSaveDAO json = new JsonSaveDAO(Application.persistentDataPath);
        json.updateCurrentLevel(levels[SceneManager.GetActiveScene().buildIndex]);
        Time.timeScale = 1;
        LevelSelect.loadMainMenu(this);
        CameraLimits.gameOverFallCamera = false;
    }
}
