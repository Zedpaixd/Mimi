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

        SceneManager.LoadScene(GameScene);
    }
    public void QuitButtonController()
    {

        Application.Quit();
    }






}
