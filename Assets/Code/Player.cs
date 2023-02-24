using UnityEngine;
using System;


[RequireComponent(typeof(Rigidbody))]
public sealed class Player : MonoBehaviour
{
    [SerializeReference] private PlayerMove _move = new();
    [SerializeReference] private LocalInput _input = new();
    [SerializeField] private GameCamera _gameCamera;
    [SerializeField] private Weapon _weapon;


    private void Awake()
    {
        Rigidbody rigid = GetComponent<Rigidbody>();

        AttachInputs();
        EquipWeapon(_weapon);
        _move.Init(rigid, transform);
    }

    private void Update()
    {
        _input.Update();
    }

    private void FixedUpdate()
    {
        float yawValue;

        if (_gameCamera is not null)
        {
            yawValue = _gameCamera.transform.rotation.eulerAngles.y;

            _move.AttachedRigidbody.MoveRotation(Quaternion.AngleAxis(yawValue, Vector3.up));
        }
            
        _move.FixedUpdate();
    }

    private void AttachInputs()
    {
        const float ForceShot = 300f;

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

            _gameCamera.SetScreenPosition(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        }, KeyMode.Down, KeyCode.F);

        _input.AttachAction(-1, () =>
        {
            _weapon.Shoot(transform.position.GetNormalTo(_gameCamera.GetAimPosition()) * ForceShot);

        }, KeyMode.Hold, KeyCode.Mouse0);
    }

    //---------weapon holder

    private void EquipWeapon(Weapon weapon)
    {
        if (weapon is null)
            return;

        weapon.transform.parent = _gameCamera.transform;
        weapon.transform.localPosition = new Vector3(0.15f,0f,0.30f);
    }
}
