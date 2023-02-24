using System;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] private int _ammoCount = 100;
    [SerializeField] private int _ammoClipSize = 10;
    [SerializeField] private float _shootDelay = 0.5f;
    [SerializeField] private float _reloadDelay = 3.0f;
    [SerializeField] private bool _isAutoReloading = true;
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

    protected virtual void OnShoot(Vector3 direction) 
    {
        int validAmmo = CalculateAvailableClipAmmo(1);

        if(validAmmo == 1)
        {
            DescreaseClipAmmo(validAmmo);
            Projectile projectile = CreateBullet(MainShootMain.position);
            projectile.SetForce(direction);
        }
 
    }

    protected virtual void OnWhileAfterShoot(float currentDelayMS, float maxDelay) { }

    protected virtual void OnReload() { }

    protected virtual void OnWhileReload(float currentDelayMS, float maxDelay) { }

    protected virtual void OnAmmoZeroReach() { }

    protected virtual void OnAmmoClipZeroReached() { }

    protected virtual Projectile CreateBullet(Vector3 position)
    {
        return Instantiate(MainProjectilePrefab, position, Quaternion.identity);
    }

    protected void RedistributeAmmoToClip()
    {
        int neededAmmoForClip;

        if(_ammoCount <= 0)
        {
            OnAmmoZeroReach();
            AmmoClipZeroReached?.Invoke();

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
            OnAmmoClipZeroReached();
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
            OnShoot(direction);
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
