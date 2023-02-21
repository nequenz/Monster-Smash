using UnityEngine;
using System;

[Serializable]
public class CameraScreenView
{
    [SerializeField] private GameCamera _camera;
    [SerializeField] private RectTransform _aimUI;


    public void CalibrateAimUI() => _aimUI.position = _camera.ScreenPosition;
}