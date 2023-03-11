using UnityEngine;
using System;

[Serializable]
public class WeaponPolicy
{
    [SerializeField] private int _ammoCount = 100;
    [SerializeField] private int _ammoClipSize = 50;
    [SerializeField] private float _reloadDelay = 3.0f;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private bool _isAutoReloading = true;
    [SerializeField] private Vector3 _angleShotSpread;
    [SerializeField] private EachFrameTimer _reloadDelayTimer = new();
    [SerializeField] private ProjectileBasic _mainProjectilePrefab;
    [SerializeField] private Transform _mainShootMain;
    private int _currentAmmoClip = 0;


    public int AmmoCount => _ammoCount;
    public int AmmoClipSize => _ammoClipSize;
    public int CurrentAmmoClip => _currentAmmoClip;
    public bool IsReloading => _reloadDelayTimer.IsRunning;
    public float ProjectileSpeed => _projectileSpeed;
    public Vector3 SpreadAngles => _angleShotSpread;
    public ProjectileBasic ProjectilePrefab => _mainProjectilePrefab;
    public Transform MainShootMain => _mainShootMain;


    public WeaponPolicy()
    {
        RedistributeAmmoToClip();
    }

    public ProjectileBasic CreateProjectile(Vector3 position)
    {
        return GameObject.Instantiate(_mainProjectilePrefab, position, Quaternion.identity);
    }

    public void RedistributeAmmoToClip()
    {
        int neededAmmoForClip;

        if (_ammoCount <= 0)
            return;

        neededAmmoForClip = _ammoClipSize - _currentAmmoClip;
        _ammoCount -= neededAmmoForClip;

        if (_ammoCount < 0)
        {
            _ammoCount = 0;
            neededAmmoForClip += _ammoCount;
        }

        _currentAmmoClip = neededAmmoForClip;
    }

    public void DescreaseClipAmmo(int count)
    {
        _currentAmmoClip = Mathf.Clamp(_currentAmmoClip - count, 0, int.MaxValue);

        if (_currentAmmoClip == 0)
        {
            if (_isAutoReloading && _currentAmmoClip > 0)
                Reload();
            else
                RedistributeAmmoToClip();
        }
    }

    public int CalculateAvailableClipAmmo(int countToDecrease)
    {
        int availableClipAmmo = _currentAmmoClip - countToDecrease;

        return availableClipAmmo >= 0
            ? countToDecrease : countToDecrease + (availableClipAmmo);
    }

    public Vector3 CalculateSpread(Vector3 nativeDirection, Vector3 angles)
    {
        Vector3 anglesSpread = default;

        angles /= 2;
        anglesSpread.x = UnityEngine.Random.Range(-angles.x, angles.x);
        anglesSpread.y = UnityEngine.Random.Range(-angles.y, angles.y);
        anglesSpread.z = UnityEngine.Random.Range(-angles.z, angles.z);

        return Quaternion.Euler(anglesSpread) * nativeDirection;
    }

    public void SetReloadMode(bool isAuto)
    {
        _isAutoReloading = isAuto;
    }

    public void AddAmmoCount(int count)
    {
        _ammoCount += count;
    }

    public void SetAmmoCount(int count)
    {
        _ammoCount = count;
    }

    public void Reload()
    {
        if (IsReloading == false)
            _reloadDelayTimer.Start();
    }

}