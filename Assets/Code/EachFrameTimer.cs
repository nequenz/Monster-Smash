using UnityEngine;
using System;


[Serializable]
public class EachFrameTimer
{
    private Action<float, float> _action;
    private Action _finishAction;
    private float _currentTime;
    private float _timeToRun;


    public event Action Started;
    public event Action Finished;


    public bool IsRunning => _currentTime > 0.0f;


    public void Set(float timeToRun, Action<float, float> action, Action finishAction)
    {
        _timeToRun = timeToRun;
        _action = action;
        _finishAction = finishAction;
    }

    public void Start()
    {
        Started?.Invoke();
        _currentTime = _timeToRun;
    }

    public void Stop()
    {
        _currentTime = 0.0f;
    }

    public void Update(float deltaTime)
    {
        if(_currentTime > 0.0f && _action is not null)
        {
            _currentTime -= deltaTime;
            _action(_currentTime, _timeToRun);

            if (_currentTime <= 0.0f)
            {
                if (_finishAction is not null)
                    _finishAction();

                Finished?.Invoke();
            }
                
        }
    }
}