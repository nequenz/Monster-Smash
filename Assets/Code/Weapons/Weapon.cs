using System;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private int _ammoCount = 100;
    [SerializeField] private int _ammoClipSize = 10;
    [SerializeField] private float _shootDelay = 0.5f;
    [SerializeField] private float _reloadDelay = 3.0f;
    [SerializeField] private bool _isAutoReloading = true;
    [SerializeField] private Vector3 _firstPersonOffset;
    [SerializeField] private float _projectileSpeed;
    private EachFrameTimer _shootDelayTimer = new();
    private EachFrameTimer _reloadDelayTimer = new();
    private int _currentAmmoClip = 0;
    private bool _isReloading = false;
    private bool _isAfterShoot = false;
    
    
    [SerializeField] protected Projectile MainProjectilePrefab;
    [SerializeField] protected Transform MainShootMain;


    public event Action Shooted;
    public event Action Reloaded;
    public event Action AmmoClipZeroReached;
    public event Action AmmoZeroReached;


    public int AmmoCount => _ammoCount;
    public int AmmoClipSize => _ammoClipSize;
    public int CurrentAmmoClip => _currentAmmoClip;
    public bool IsReloading => _isReloading;
    public bool IsAfterShoot => _isAfterShoot;
    public float ProjectileSpeed => _projectileSpeed;
    public Vector3 FirstPersonViewOffset => _firstPersonOffset;


    private void Awake()
    {
        InitTimers();
        RedistributeAmmoToClip();
    }

    private void Update()
    {
        _shootDelayTimer.Update(Time.deltaTime);
        _reloadDelayTimer.Update(Time.deltaTime);
    }

    private void InitTimers()
    {
        _shootDelayTimer.Set(_shootDelay, OnWhileAfterShoot, () => _isAfterShoot = false);

        _reloadDelayTimer.Set(_reloadDelay, OnWhileReload, () =>
        {
            _isReloading = false;
            RedistributeAmmoToClip();
        });
    }

    protected abstract void OnAwake();

    protected abstract void OnUpdate();

    protected abstract void OnShoot(Vector3 direction);

    protected abstract void OnWhileAfterShoot(float currentDelayMS, float maxDelay) ;

    protected abstract void OnReload();

    protected abstract void OnWhileReload(float currentDelayMS, float maxDelay);

    protected abstract void OnAmmoZeroReach();

    protected abstract void OnAmmoClipZeroReach();

    protected virtual Projectile CreateProjectile(Vector3 position)
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

    protected void RedistributeAmmoToClip()
    {
        int neededAmmoForClip;

        if(_ammoCount <= 0)
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
        else
        {
            _isAfterShoot = true;
            _shootDelayTimer.Start();
        }
    }

    protected int CalculateAvailableClipAmmo(int countToDecrease)
    {
        int availableClipAmmo = _currentAmmoClip - countToDecrease;

        return availableClipAmmo >= 0
            ? countToDecrease : countToDecrease + (availableClipAmmo);
    }

    public void EquipToObject(Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = _firstPersonOffset;
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

    public void Shoot(Vector3 direction)
    {
        if(_isAfterShoot == false && _isReloading == false)
        {
            OnShoot(direction.normalized * _projectileSpeed);
            Shooted?.Invoke();
        }
   
    }

    public void Reload()
    {
        if(_isAfterShoot == false && _isAfterShoot == false)
        {
            _isReloading = true;
            _reloadDelayTimer.Start();

            OnReload();
            Reloaded?.Invoke();
        }
    }
}
