using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonSaveDAO
{
    public static string saveName = "GameInfo.json";
    string path;
    public SaveJsonModel model { get; }
    public JsonSaveDAO(string path)
    {
        model = new SaveJsonModel();
        path += "/GameInfo.json";
        this.path = path;
        if (!File.Exists(path) || getModelFromJson() == null)
        {
            File.Create(path).Close();
            setModel(model);
        }
        else
        {
            model = getModelFromJson();
        }
    }
    private void setModel(SaveJsonModel model)
    {
        File.WriteAllText(path, JsonUtility.ToJson(model));
    }

    public void updateMusicVolume(float value)
    {
        model.musicVolume = value;
        setModel(model);
        Debug.Log(JsonUtility.ToJson(model));

    }
    public void updateSfxVolume(float value)
    {
        model.sfxVolume = value;
        setModel(model);
        Debug.Log(JsonUtility.ToJson(model));

    }

    public void updateCurrentLevel(string value)
    {
        model.currentLevel = value;
        setModel(model);
        Debug.Log(JsonUtility.ToJson(model));

    }


    SaveJsonModel getModelFromJson()
    {
        return JsonUtility.FromJson<SaveJsonModel>(File.ReadAllText(path));
    }
    public string getCurrentLevelFromJson()
    {
        return JsonUtility.FromJson<SaveJsonModel>(File.ReadAllText(path)).currentLevel;
    }
    public float getMusicVolumeFromJson()
    {
        return JsonUtility.FromJson<SaveJsonModel>(File.ReadAllText(path)).musicVolume;
    }
    public float getSfxVolumeFromJson()
    {
        return JsonUtility.FromJson<SaveJsonModel>(File.ReadAllText(path)).sfxVolume;
    }

}

