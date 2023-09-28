using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : Component, new()
{
    private static readonly object lock_obj = new();

    private static T _instance;

    public static T Instance
    {
        get
        {
            lock (lock_obj)
            {
                if (_instance == null)
                    _instance = Object.FindObjectOfType<T>();

                return _instance;
            }
        }
        internal set { _instance = value; }
    }
}
