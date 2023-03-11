using UnityEngine;


public sealed class ItemM4 : Item
{
    [SerializeReference, SubclassSelector] private WeaponPolicy _weapon = new();
    [SerializeField] private ParticleSystem _afterShotVFX;
    private Vector3 _lastShotDirection;
    private Vector3 _directionToShoot;


    protected override void OnWhileUsing(float currentDelay, float maxDelay)
    {
        
    }

    public override void Use()
    {
        int validAmmo;

        if (IsReadyToUse() == false)
            return;

        validAmmo = _weapon.CalculateAvailableClipAmmo(1);

        if (validAmmo == 1)
        {
            ProjectileBasic projectile = _weapon.CreateProjectile(_weapon.MainShootMain.position);
            _lastShotDirection = _directionToShoot;

            _afterShotVFX.Play();
            _weapon.DescreaseClipAmmo(validAmmo);
            projectile.SetInitialShotForce(_weapon.CalculateSpread(_directionToShoot, _weapon.SpreadAngles));
            StartUsingTimer();
        }
    }

    public override void AcceptDispatcher(IItemDispatcher itemDispatcher)
    {
        itemDispatcher.Dispatch(this);
    }

    public void SetDirectionShoot(Vector3 direction)
    {
        _directionToShoot = direction;
    }
}