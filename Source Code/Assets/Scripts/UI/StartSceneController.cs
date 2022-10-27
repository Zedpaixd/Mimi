using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    [SerializeField] string GameScene;

    public void StartButtonController()
    {
        JsonSaveDAO saveJson = new JsonSaveDAO(Application.persistentDataPath);
        Debug.Log(saveJson.getCurrentLevelFromJson());
        LevelSelect.StartLevel(saveJson.getCurrentLevelFromJson(), this);
    }
    public void QuitButtonController()
    {
        Application.Quit();
    }






}
