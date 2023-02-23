using System;
using UnityEngine;


public class Weapon
{
    private Transform _shotPoint;
    private Projectile _projectile;
    private float _shootDelayMax = 1.0f;
    private float _shootDelay = 0.0f;
    private int _ammoMax = 9999;
    private int _ammoCurrent = 0;


    public event Action Shooted;


    public int AmmoMax => _ammoMax;
    public int AmmoCurrent => _ammoCurrent;
    public float ShootDelay => _shootDelayMax;


    public void Init(Transform shotPoint, Projectile projectile, float shootDelayMax, int ammoMax)
    {
        _shotPoint = shotPoint;
        _projectile = projectile;
        _shootDelayMax = shootDelayMax;
        _ammoMax = ammoMax;
    }

    public void SetAmmo(int ammo) => _ammoCurrent = ammo;

    public Projectile Shoot(Vector3 direction)
    {
        Projectile proj = null;

        if (_shootDelay <= 0.0f)
        {
            proj = GameObject.Instantiate(_projectile, _shotPoint.position, Quaternion.identity);
            _ammoCurrent = Mathf.Clamp(_ammoCurrent - 1, 0, _ammoMax);
            _shootDelay = _shootDelayMax;

            proj.SetForce(direction);
            Shooted?.Invoke();
        }

        return proj;
    }

    public void Update(float deltaTime)
    {
        if (_shootDelay > 0.0f)
            _shootDelay -= deltaTime;
    }
}