using UnityEngine;

[DefaultExecutionOrder(-1)]
public abstract class Actor<T> : MonoBehaviour where T : Manager<T>
{
    protected T Manager;

    protected virtual void OnEnable()
    {
        Manager = FindObjectOfType<T>();

        ConfigureSubscriptions(true);
    }

    protected virtual void OnDisable()
    {
        ConfigureSubscriptions(false);
    }

    protected virtual void ConfigureSubscriptions(bool status) { }
}
