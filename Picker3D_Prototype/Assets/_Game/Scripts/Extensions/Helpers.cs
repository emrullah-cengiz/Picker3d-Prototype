﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class Helpers
{
    public static IEnumerator ExecuteOnEndOfFrame(Action action)
    {
        yield return new WaitForEndOfFrame();

        action();

        yield break;
    }

    public static void Subscribe(this UnityEvent signal, UnityAction subscriber, bool status)
    {
        if (status)
            signal.AddListener(subscriber);
        else
            signal.RemoveListener(subscriber);
    }

    public static void Subscribe<T0>(this UnityEvent<T0> signal, UnityAction<T0> subscriber, bool status)
    {
        if (status)
            signal.AddListener(subscriber);
        else
            signal.RemoveListener(subscriber);
    }

    public static void Subscribe<T0, T1>(this UnityEvent<T0, T1> signal, UnityAction<T0, T1> subscriber, bool status)
    {
        if (status)
            signal.AddListener(subscriber);
        else
            signal.RemoveListener(subscriber);
    }
}
