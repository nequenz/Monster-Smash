

public abstract class DefaultCleanComponent : ICleanComponent
{
    private bool _isEnabled = false;
    private RootComponent _root;

    public bool IsEnabled => _isEnabled;


    protected abstract void OnInit();

    protected void Enable() => _isEnabled = true;

    protected void Disable() => _isEnabled = false;

    public void Init(RootComponent root)
    {
        _root = root;
        OnInit();
    }

    public T FindComponent<T>() => _root.FindComponent<T>();

    public abstract void Update();

    public abstract void FixedUpdate();
}