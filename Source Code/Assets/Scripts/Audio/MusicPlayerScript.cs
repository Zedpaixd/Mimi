using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[Serializable]
public class SaveData
{
    public float vol;
    public static string saveName = "Volume.json";

    public SaveData(float vol)
    {
        this.vol = vol;
    }
    public static void saveVolume(string path, SaveData sd)
    {
        string _path = Path.Combine(path);

        if (!File.Exists(_path))
        {
            File.Create(_path).Close();
        }

        string json = JsonUtility.ToJson(sd, true);
        Debug.Log(json);
        StreamWriter sw = new StreamWriter(_path);
        sw.Write(json);
        sw.Close();
        Debug.Log(string.Format("Wrote save data to {0}", _path));
    }
    public static SaveData LoadVolume(string path)
    {
        SaveData sd;
        if (!File.Exists(path))
        {
            sd = new SaveData(1f);
        }
        else
        {
            StreamReader sr = new StreamReader(path);
            sd = JsonUtility.FromJson<SaveData>(sr.ReadToEnd());
            sr.Close();
        }
        return sd;
    }
}

public class MusicPlayerScript : MonoBehaviour
{

    public GameObject musicObject;
    public Slider volumeSlider;
    private AudioSource AudioSource;
    private SaveData json;

    private float musicVolume = 1f;

    void Start()
    {
        musicObject = GameObject.FindWithTag("Audio");
        //AudioSource = musicObject.GetComponent<AudioSource>();
        AudioSource = AudioManager.Instance.source;
        json = new SaveData(musicVolume);

        json = SaveData.LoadVolume(Path.Combine(Application.persistentDataPath,SaveData.saveName));
        AudioSource.volume = json.vol;
        volumeSlider.value = json.vol;
        
    }

    void Update()
    {

    }

    public void updateVolume(float volume)
    {
        AudioSource.volume = volume;
        json.vol = volume;
    }

    public void saveVolume()
    {
        SaveData.saveVolume(Path.Combine(Application.persistentDataPath, SaveData.saveName), json);
    }
}
