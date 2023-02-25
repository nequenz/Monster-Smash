using System;
using UnityEngine;


public abstract class ItemWeapon : Item
{
    [Header("Weapon params")]
    [SerializeField] private int _ammoCount = 100;
    [SerializeField] private int _ammoClipSize = 10;
    [SerializeField] private float _reloadDelay = 3.0f;
    [SerializeField] private bool _isAutoReloading = true;
    [SerializeField] private float _projectileSpeed;
    private EachFrameTimer _reloadDelayTimer = new();
    private int _currentAmmoClip = 0;


    public event Action Shooted;
    public event Action Reloaded;
    public event Action AmmoClipZeroReached;
    public event Action AmmoZeroReached;


    [SerializeField] protected ProjectileBasic MainProjectilePrefab;
    [SerializeField] protected Transform MainShootMain;
    public int AmmoCount => _ammoCount;
    public int AmmoClipSize => _ammoClipSize;
    public int CurrentAmmoClip => _currentAmmoClip;
    public bool IsReloading => _reloadDelayTimer.IsRunning;
    public float ProjectileSpeed => _projectileSpeed;


    protected override void Awake()
    {
        base.Awake();
        _reloadDelayTimer.Set(_reloadDelay, OnWhileReload, () => RedistributeAmmoToClip());
        RedistributeAmmoToClip();
    }

    protected override void Update()
    {
        base.Update();
        _reloadDelayTimer.Update(Time.deltaTime);
    }

    protected void RedistributeAmmoToClip()
    {
        int neededAmmoForClip;

        if (_ammoCount <= 0)
        {
            OnAmmoZeroReach();
            AmmoZeroReached?.Invoke();

            return;
        }

        neededAmmoForClip = _ammoClipSize - _currentAmmoClip;
        _ammoCount -= neededAmmoForClip;

        if (_ammoCount < 0)
        {
            _ammoCount = 0;
            neededAmmoForClip += _ammoCount;
        }

        _currentAmmoClip = neededAmmoForClip;
    }

    protected void DescreaseClipAmmo(int count)
    {
        _currentAmmoClip = Math.Clamp(_currentAmmoClip - count, 0, int.MaxValue);

        if (_currentAmmoClip == 0)
        {
            OnAmmoClipZeroReach();
            AmmoClipZeroReached?.Invoke();

            if (_isAutoReloading && _currentAmmoClip > 0)
                Reload();
            else
                RedistributeAmmoToClip();
        }
    }

    protected int CalculateAvailableClipAmmo(int countToDecrease)
    {
        int availableClipAmmo = _currentAmmoClip - countToDecrease;

        return availableClipAmmo >= 0
            ? countToDecrease : countToDecrease + (availableClipAmmo);
    }

    protected virtual ProjectileBasic CreateProjectile(Vector3 position)
    {
        return Instantiate(MainProjectilePrefab, position, Quaternion.identity);
    }

    protected Vector3 CalculateSpread(Vector3 nativeDirection, Vector3 angles)
    {
        Vector3 anglesSpread = default;

        angles /= 2;
        anglesSpread.x = UnityEngine.Random.Range(-angles.x, angles.x);
        anglesSpread.y = UnityEngine.Random.Range(-angles.y, angles.y);
        anglesSpread.z = UnityEngine.Random.Range(-angles.z, angles.z);

        return Quaternion.Euler(anglesSpread) * nativeDirection;
    }

    protected abstract void OnShoot(Vector3 direction);

    protected abstract void OnReload();

    protected abstract void OnWhileReload(float currentDelayMS, float maxDelay);

    protected abstract void OnAmmoZeroReach();

    protected abstract void OnAmmoClipZeroReach();

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

    public void Shoot(Vector3 direction)
    {
        if (IsUsing == false && IsReloading == false)
        {
            OnShoot(direction.normalized * _projectileSpeed);
            Shooted?.Invoke();
        }
    }

    public void Reload()
    {
        if (IsReloading == false && IsUsing == false)
        {
            _reloadDelayTimer.Start();

            OnReload();
            Reloaded?.Invoke();
        }
    }
}