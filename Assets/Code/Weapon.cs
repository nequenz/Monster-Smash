using System;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] private int _ammoCount = 100;
    [SerializeField] private int _ammoClipSize = 10;
    [SerializeField] private float _shootDelay = 0.5f;
    [SerializeField] private float _reloadDelay = 3.0f;
    private int _currentAmmoClip = 0;


    [SerializeField] protected Projectile MainProjectilePrefab;
    [SerializeField] protected Transform MainShootMain;


    public event Action Shooted;
    public event Action Reloaded;
    public event Action AmmoClipZeroReached;
    public event Action AmmoZeroReached;


    public int AmmoCount => _ammoCount;
    public int AmmoClipSize => _ammoClipSize;
    public int CurrentAmmoClip => _currentAmmoClip;


    private void Awake()
    {
        RedistributeAmmoToClip();
    }

    protected virtual void OnShoot() 
    {
        int validAmmo = CalculateAvailableClipAmmo(1);

        DescreaseClipAmmo(validAmmo);
        CreateBullet(MainShootMain.position);

    }

    protected virtual void OnReload() 
    {

    }

    protected virtual void OnAmmoZeroReach() { }

    protected virtual void OnAmmoClipZeroReached() { }

    protected Projectile CreateBullet(Vector3 position)
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
        _currentAmmoClip = Math.Clamp(_currentAmmoClip, 0, int.MaxValue);

        if(_currentAmmoClip == 0)
        {
            OnAmmoClipZeroReached();
            AmmoClipZeroReached?.Invoke();
        }
    }

    protected int CalculateAvailableClipAmmo(int countToDecrease)
    {
        int availableClipAmmo = _currentAmmoClip - countToDecrease;

        return availableClipAmmo >= 0
            ? countToDecrease : countToDecrease + (availableClipAmmo);
    }

    public void AddAmmoCount(int count)
    {
        _ammoCount += count;
    }

    public void SetAmmoCount(int count)
    {
        _ammoCount = count;
    }

    public void Shoot()
    {
        OnShoot();
    }

    public void Reload()
    {
        OnReload();
    }
}
