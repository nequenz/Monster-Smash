using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ComponentDelegate<T>(T instance);

public class RootComponent : MonoBehaviour
{
    private List<ICleanComponent> _components = new();

    private void Awake()
    {
        
    }

    protected void InitComponents() => _components.ForEach((component) => component.Init(this));

    protected void UpdateComponent()
    {
        _components.ForEach((component) =>
        {
            if (component.IsEnabled)
                component.Update();
        });
    }

    protected void FixedUpdateComponent()
    {
        _components.ForEach((component) =>
        {
            if (component.IsEnabled)
                component.FixedUpdate();
        });
    }

    protected void AddComponent(ICleanComponent component) => _components.Add(component);

    public T FindComponent<T>()  => (T)_components.Find((component) => component is T);
}
