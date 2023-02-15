using UnityEngine;
using System;

[Serializable]
public class AimView
{
    [SerializeField] private CameraRaycast _look;
    [SerializeField] private RectTransform _aimUI;

    public void Init()
    {
        if (_look is not null && _aimUI is not null)
            _aimUI.position = _aimUI.position;
            
    }
}