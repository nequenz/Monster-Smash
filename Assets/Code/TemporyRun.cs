using System;


public sealed class TemporyRun
{
    private Action<TemporyRun> _action;
    private Action<TemporyRun> _actionBefore;
    private Action<TemporyRun> _actionAfter;
    private float _timeToRun;
    private float _currentTime = 0.0f;
    private bool _canBeInterrupted = false;


    public float TimeToRun => _timeToRun;
    public float CurrentTime => _currentTime;
    public bool CanBeInterrupted => _canBeInterrupted;


    public void SetInterruptMode(bool canBeInterrupted) => _canBeInterrupted = canBeInterrupted;

    public void Select(Action<TemporyRun> action, float timeToRun)
    {
        _action = action;
        _timeToRun = timeToRun;
    }

    public void SelectActionBefore(Action<TemporyRun> action) => _actionBefore = action;

    public void SelectActionAfter(Action<TemporyRun> action) => _actionAfter = action;

    public void Update(float deltaTime)
    {
        if (_currentTime > 0.0f)
        {
            _action?.Invoke(this);
            _currentTime -= deltaTime;

            if (_currentTime <= 0.0f)
                Stop();
        }
    }

    public void Start()
    {
        if(_canBeInterrupted == true)
        {
            Stop();
            _currentTime = _timeToRun;
            _actionBefore?.Invoke(this);
        }
        else if(_canBeInterrupted == false && _currentTime <= 0.0f)
        {
            _currentTime = _timeToRun;
            _actionBefore?.Invoke(this);
        }
    }

    public void Stop()
    {
        _currentTime = 0.0f;
        _actionAfter?.Invoke(this);
    }
}
