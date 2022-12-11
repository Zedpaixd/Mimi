using System.Collections;
using System.Collections.Generic;
using System;
public class SaveJsonModel
{

    public float musicVolume;
    public float sfxVolume;
    public string currentLevel;

    public SaveJsonModel(float musicVolume, float sfxVolume, string currentLevel)
    {
        this.musicVolume = musicVolume;
        this.sfxVolume = sfxVolume;
        this.currentLevel = currentLevel;
    }

    public SaveJsonModel()
    {
        this.musicVolume = 0.5f;
        this.sfxVolume = 0.5f;
        this.currentLevel = "Level 1";
    }


    public override string ToString()
    {
        return base.ToString() + " musicVolume=" + musicVolume + ", sfxVolume=" + sfxVolume + ", LastLevelBeaten=" + currentLevel;
    }
}