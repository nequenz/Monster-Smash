using UnityEngine;
using System;


public class RootComponent : MonoBehaviour
{
    public event Action Started;
    public event Action Enabled;
    public event Action Disabled;
    public event Action Updated;
    public event Action FixedUpdated;


    protected void InvokeStartEvent() => Started?.Invoke();

    protected void InvokeEnableEvent() => Enabled?.Invoke();

    protected void InvokeDisableEvent() => Disabled?.Invoke();

    protected void InvokeUpdateEvent() => Updated?.Invoke();

    protected void InvokeFixedUpdateEvent() => FixedUpdated?.Invoke();

}

