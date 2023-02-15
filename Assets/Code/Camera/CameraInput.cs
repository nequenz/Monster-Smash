using System;
using UnityEngine;


[Serializable]
public class CameraInput
{
    [Header("Camera pitch")]
    [SerializeField] private float _pitchViewUp = 80.0f;
    [SerializeField] private float _pitchViewDown = -80.0f;

    [Header("Mouse move factor")]
    [SerializeField] private Vector2 _axiesFactor = new Vector2(2.35f, 1.15f);

    private Vector2 _angles;
    private Quaternion _additinalRotate = Quaternion.identity;

    public void Init()
    {

    }

    public void Update()
    {
        _angles.x -= Input.GetAxis("Mouse X") * _axiesFactor.x;
        _angles.y -= Input.GetAxis("Mouse Y") * _axiesFactor.y;
        _angles.y = Mathf.Clamp(_angles.y, _pitchViewDown, _pitchViewUp);
    }
}