using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : Component
{

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log(string.Format("{0} instance already exists", Instance.ToString()));
            Destroy(this);
            Destroy(gameObject);
        }
    }

    /*protected void ResetList<U>(List<U> list) where U : IDisposable
    {
        if (list == null) return;

        for (int i = 0; i < list.Count; i++)
        {
            list[i].Dispose();
        }

        list.Clear();
    }*/

    //protected void ResetList<V>(List<V> list) where V : Component
    //{
    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        GameObject go = list[i].gameObject;
    //        if (go)
    //            Destroy(go);
    //    }

    //    list.Clear();
    //}

    /*protected void ResetList(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = list[i];
            if (go)
                Destroy(go);
        }

        list.Clear();
    }*/
}