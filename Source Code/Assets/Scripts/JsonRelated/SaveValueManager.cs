using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveValueManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] List<AudioSource> sfxSounds;

    public List<AudioSource> SfxSounds { get => sfxSounds; set => sfxSounds = value; }
    public AudioSource MusicSource { get => musicSource; set => musicSource = value; }

    private void Start()
    {
        JsonSaveDAO GameSaveInfo = new JsonSaveDAO(Application.persistentDataPath);
        //Debug.Log(SceneManager.GetSceneByName(GameSaveInfo.getCurrentLevelFromJson()).buildIndex);

        musicSource = Camera.main.GetComponentInChildren<AudioSource>();
        musicSource.volume = GameSaveInfo.getMusicVolumeFromJson();
        if (sfxSounds.Count > 0)
        {
            sfxSounds.ForEach(sfx => sfx.volume = GameSaveInfo.getSfxVolumeFromJson());
        }
        if (SceneManager.GetActiveScene().buildIndex > SceneManager.GetSceneByName(GameSaveInfo.getCurrentLevelFromJson()).buildIndex && SceneManager.GetActiveScene().name != "Main Menu")
        {
            GameSaveInfo.updateCurrentLevel(SceneManager.GetActiveScene().name);
        }
    }
}