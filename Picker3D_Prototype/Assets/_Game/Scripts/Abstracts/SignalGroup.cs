using UnityEngine;

namespace Assets._Game.Scripts.Abstracts
{
    [DefaultExecutionOrder(-5)]
    public class SignalGroup<T> : MonoSingleton<T> where T : Component, new()
    {
    }
}
