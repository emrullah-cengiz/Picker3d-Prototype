using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-3)]
public abstract class Manager<T> : MonoBehaviour where T : Component
{
    protected virtual void OnEnable()
    {
        ConfigureSubscriptions(true);
    }

    protected virtual void OnDisable()
    {
        ConfigureSubscriptions(false);
    }

    protected virtual void ConfigureSubscriptions(bool status) { }
}
