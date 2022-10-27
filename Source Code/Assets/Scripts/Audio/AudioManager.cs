using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : GenericSingleton<AudioManager>
{
    public AudioSource source;
    private float progress;

    public override void Awake()
    {
        base.Awake();
        source = gameObject.AddComponent<AudioSource>();
        SceneManager.sceneLoaded += findAudio; //(scene, mode) => { source.clip = FindObjectOfType<SceneAudio>().GetBGM(); source.Play(); };
        findAudio(SceneManager.GetActiveScene(),LoadSceneMode.Single);
        SceneManager.sceneUnloaded += UpdateProgress;
    }

    private void findAudio(Scene scene, LoadSceneMode mode)
    {

        source.clip = FindObjectOfType<SceneAudio>().GetBGM();

        if (scene.buildIndex == 1 || scene.buildIndex == 0)
        {
            source.time = progress;
        }

        source.Play();

    }

    private void UpdateProgress(Scene scene)
    {
        progress = source.time;
    }




}
