using UnityEngine;
using System;


[RequireComponent(typeof(Rigidbody))]
public sealed class Player : MonoBehaviour
{
    [SerializeReference] private PlayerMove _move = new();
    [SerializeReference] private WeaponBuilder _weaponBuilder = new();
    [SerializeReference] private WeaponHolder _weaponHolder = new();
    [SerializeReference] private LocalInput _input = new();
    [SerializeField] private GameCamera _firstPersonCamera;
    private float TargetDistanceToShoot = 10.0f;
    

    private void Awake()
    {
        Rigidbody rigid = GetComponent<Rigidbody>();

        AttachInputs();
        _move.Init(rigid, transform);
        _weaponHolder.SetWeapon(_weaponBuilder.CreateTestWeapon());

    }

    private void Update()
    {
        _input.Update();
        _weaponHolder.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        float yawValue;

        if (_firstPersonCamera is not null)
        {
            yawValue = _firstPersonCamera.transform.rotation.eulerAngles.y;

            _move.AttachedRigidbody.MoveRotation(Quaternion.AngleAxis(yawValue, Vector3.up));
        }
            
        _move.FixedUpdate();
    }

    private void AttachInputs()
    {
        const float ForceShot = 250f;

        _input.AttachAction(-1, () => _move.Move(_move.AttachedTransform.forward), KeyMode.Hold, KeyCode.W);
        _input.AttachAction(-1, () => _move.Move(_move.AttachedTransform.forward * -1), KeyMode.Hold, KeyCode.S);
        _input.AttachAction(-1, () => _move.Move(_move.AttachedTransform.right), KeyMode.Hold, KeyCode.D);
        _input.AttachAction(-1, () => _move.Move(_move.AttachedTransform.right * -1), KeyMode.Hold, KeyCode.A);
        _input.AttachAction(-1, () => _move.Jump(), KeyMode.Hold, KeyCode.Space);

        _input.AttachAction(-1, () =>
        {
            if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;

        }, KeyMode.Down, KeyCode.F);

        _input.AttachAction(-1, () =>
        {
            Vector3 direction = _weaponBuilder.ShotPoint.position.GetNormalTo(_firstPersonCamera.CalculatePositionByDistance(TargetDistanceToShoot));
            _weaponHolder.Shoot(direction * ForceShot);

        }, KeyMode.Hold, KeyCode.Mouse0);
    }


}
