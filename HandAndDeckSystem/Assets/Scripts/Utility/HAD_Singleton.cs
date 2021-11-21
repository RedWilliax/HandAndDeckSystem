using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_Singleton<T> : MonoBehaviour  where T : MonoBehaviour
{

    static T instance = default(T);

    public static T Instance
    {
        get
        {
            if (instance == null) return default(T);
            return instance;
        }
    }

    protected virtual void Awake()
    {
        InitSingletion();
    }

    void InitSingletion()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this as T;
        name += $"[{typeof(T).Name}]";
    }

}
