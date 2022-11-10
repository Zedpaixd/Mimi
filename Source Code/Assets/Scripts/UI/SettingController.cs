using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingController : MonoBehaviour
{

    //public GameObject musicObject;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    JsonSaveDAO GameSaveInfo;
    AudioSource musicSource;
    [SerializeField] List<AudioSource> sfxSounds;

    // private AudioSource AudioSource;
    private float musicVolume;
    private float sfxVolume;
    private void Awake()
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
        musicVolume = volume / 100f;
        musicSource.volume = musicVolume;
    }
    //dynamic float
    public void SfxVolume(float volume)
    {
        sfxVolume = volume / 100f;
        sfxSounds.ForEach(sfx => sfx.volume = volume);
    }

    //If setting frame is actives
       private void OnEnable()
       {
           if (musicVolumeSlider != null)
           {
               GameSaveInfo = new JsonSaveDAO(Application.persistentDataPath);
               musicVolumeSlider.value = GameSaveInfo.getMusicVolumeFromJson()*100;
               sfxVolumeSlider.value = GameSaveInfo.getSfxVolumeFromJson()*100;
           }
       }

    public void SaveButton()
    {
        GameSaveInfo.updateSfxVolume(sfxVolume);
        GameSaveInfo.updateMusicVolume(musicVolume);
    }


}
