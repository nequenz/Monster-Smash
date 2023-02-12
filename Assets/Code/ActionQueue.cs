using System;
using System.Collections.Generic;


public struct TempAction
{
    private Action _action;
    private float _timeToRun;


    public float TimeToRun => _timeToRun;


    public TempAction(Action action, float timeToRun)
    {
        _action = action;
        _timeToRun = timeToRun;
    }
}

public sealed class ActionQueue
{
    private List<TempAction> _actions = new();
    private float _currentActionTime = 0.0f;
    private float _actionTimeToRun = 0.0f;

    public void Add(Action action, float timeToRun)
    {
        TempAction tempAction = new TempAction(action, timeToRun);

        _actions.Add(tempAction);
    }

    public void Next()
    {
        //_actions.GetEnumerator().
    }

    public void Update()
    {
        if(_currentActionTime > 0.0f)
        {


        }
    }
}
