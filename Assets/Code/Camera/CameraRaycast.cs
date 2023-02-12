using System;
using UnityEngine;


[Serializable]
public class CameraRaycast
{
    [SerializeField] private RectTransform _aimUI;
    [SerializeField] private GameCamera _camera;
    [SerializeField] private Vector3 _screenPosition;
    [SerializeField] private bool _isAutoScreenCenter = true;


    public void Init()
    {
        if (_isAutoScreenCenter && _aimUI is not null)
            _screenPosition = _aimUI.position;
    }

    public Ray Raycast()
    {
        return _camera.AttachedCamera.ScreenPointToRay(_screenPosition);
    }
}