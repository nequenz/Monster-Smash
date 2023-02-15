using System;
using UnityEngine;


[Serializable]
public class CameraRaycast
{
    [SerializeField] private RectTransform _mainUI;
    [SerializeField] private GameCamera _camera;
    [SerializeField] private Vector3 _screenPosition;
    [SerializeField] private bool _isAutoScreenCenter = true;


    public void Init()
    {
        if (_isAutoScreenCenter && _mainUI is not null)
            _screenPosition = new Vector3( _mainUI.rect.width / 5, _mainUI.rect.height / 5 );
    }

    public void SetScreenPosition(Vector3 position) => _screenPosition = position;

    public Ray Raycast()
    {
        return _camera.AttachedCamera.ScreenPointToRay(_screenPosition);
    }
}