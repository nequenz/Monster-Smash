using System;
using UnityEngine;


[Serializable]
public class CameraInput
{
    [SerializeField] private Projectile _projectileToShoot;
    [SerializeField] private Transform _weaponProjectilePoint;
    [SerializeField] private float _reloadDelay = 0.5f;
    [SerializeField] private Vector2 _axiesFactor = new Vector2(2.35f, 1.15f);
    [SerializeField] private float _speed = 3.0f;
    GameCamera _camera;
    float _reloadValue = 0.0f;
    private Vector2 _angles;


    private bool _isReadyShoot => _reloadValue <= 0.0f;


    private void ReloadWeapon(float deltaTime)
    {
        if(_reloadValue > 0.0f)
            _reloadValue -= deltaTime;
    }

    private void Shoot()
    {
        Projectile proj;

        if (_isReadyShoot)
        {
            proj = GameObject.Instantiate(_projectileToShoot, _weaponProjectilePoint.position, Quaternion.identity).GetComponent<Projectile>();
            _reloadValue = _reloadDelay;

            proj.AddForce(_camera.Transform.forward);
        }
    }

    private void UseLaser()
    {
        Projectile proj;

        if (_isReadyShoot)
        {
            proj = GameObject.Instantiate(_projectileToShoot, _weaponProjectilePoint.position, Quaternion.identity).GetComponent<Projectile>();
            _reloadValue = _reloadDelay;

            proj.SetSpeed(200.0f);
            proj.AddForce(_weaponProjectilePoint.forward);
            Debug.DrawLine(_weaponProjectilePoint.position, _weaponProjectilePoint.position + _weaponProjectilePoint.forward, Color.red, 0.1f);
        }
    }

    public void Init(GameCamera camera)
    {
        _camera = camera;
    }

    public void Update(float deltaTime)
    {
        _angles.y += Input.GetAxis("Mouse X") * _axiesFactor.x;
        _angles.x -= Input.GetAxis("Mouse Y") * _axiesFactor.y;
        _camera.transform.rotation = Quaternion.Euler(_angles);

        if(Input.GetKey(KeyCode.W))
            _camera.Transform.position += _camera.Transform.forward * deltaTime * _speed;

        if (Input.GetKey(KeyCode.S))
            _camera.Transform.position -= _camera.Transform.forward * deltaTime * _speed;

        if (Input.GetKey(KeyCode.A))
            _camera.Transform.position -= _camera.Transform.right * deltaTime * _speed;

        if (Input.GetKey(KeyCode.D))
            _camera.Transform.position += _camera.Transform.right * deltaTime * _speed;

        if (Input.GetKeyDown(KeyCode.H))
            Cursor.visible = !Cursor.visible;

        if (Input.GetKey(KeyCode.Mouse0))
            Shoot();

        if (Input.GetKey(KeyCode.Mouse1))
            UseLaser();

        ReloadWeapon(deltaTime);
    }


}