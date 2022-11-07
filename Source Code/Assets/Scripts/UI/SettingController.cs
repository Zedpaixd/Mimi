using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using MiniJSON;

public class SettingController : MonoBehaviour
{

    //public GameObject musicObject;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    JsonSaveDAO GameSaveInfo;
    [SerializeField] AudioSource musicSource;
    [SerializeField] List<AudioSource> sfxSounds;

    // private AudioSource AudioSource;
    private float musicVolume;
    private float sfxVolume;
    private void Start()
    {
        musicSource = Camera.main.GetComponentInChildren<AudioSource>();

    }

    public void isVisible(bool visible)
    {
        gameObject.SetActive(visible);

    }
    //dynamic float
    public void MusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
    }
    //dynamic float
    public void SfxVolume(float volume)
    {
        sfxVolume = volume;
        sfxSounds.ForEach(sfx => sfx.volume = volume);
    }

    //If setting frame is actives
    private void OnEnable()
    {
        if (musicVolumeSlider != null)
        {
            GameSaveInfo = new JsonSaveDAO(Application.persistentDataPath);
            musicVolumeSlider.value = GameSaveInfo.getMusicVolumeFromJson();
            sfxVolumeSlider.value = GameSaveInfo.getSfxVolumeFromJson();
        }
    }

    public void SaveButton()
    {
        Debug.Log(sfxVolume + "  " + musicVolume);
        GameSaveInfo.updateSfxVolume(sfxVolume);
        GameSaveInfo.updateMusicVolume(musicVolume);
    }


}
