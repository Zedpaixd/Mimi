using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateValueStartScene : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] List<AudioSource> sfxSounds;
    private void start()
    {
        JsonSaveDAO GameSaveInfo = new JsonSaveDAO(Application.persistentDataPath);
        musicSource = Camera.main.GetComponentInChildren<AudioSource>();
        musicSource.volume = GameSaveInfo.getMusicVolumeFromJson();
        if (sfxSounds.Count > 0)
        {
            sfxSounds.ForEach(sfx => sfx.volume = GameSaveInfo.getSfxVolumeFromJson());
        }
        if (SceneManager.GetActiveScene().buildIndex > SceneManager.GetSceneByName(GameSaveInfo.getCurrentLevelFromJson()).buildIndex)
        {
            GameSaveInfo.updateCurrentLevel(SceneManager.GetActiveScene().name);
        }
    }
}