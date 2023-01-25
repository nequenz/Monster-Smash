


public interface ICleanComponent
{
    public bool IsEnabled { get; }


    public void Init(RootComponent root);

    public void Update();

    public void FixedUpdate();

    public T FindComponent<T>();
}