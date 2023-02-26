using UnityEngine;


public sealed class ItemWeaponM4 : ItemWeapon
{
    [Header("Concrete params")]
    [SerializeField] private Vector3 _angleShotSpread;
    [SerializeField] private ParticleSystem _afterShotVFX;
    private Vector3 _lastShotDirection;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnUse()
    {
        Vector3 directionToShoot;

        User.Is(out Player player);

        if (player is not null)
        {
            directionToShoot = MainShootMain.position.GetNormalTo(player.AttachedCamera.GetAimPosition());
            Shoot(directionToShoot);
        }
    }

    protected override void OnShoot(Vector3 direction)
    {
        int validAmmo = CalculateAvailableClipAmmo(1);

        if (validAmmo == 1)
        {
            ProjectileBasic projectile = CreateProjectile(MainShootMain.position);
            _lastShotDirection = direction;

            _afterShotVFX.Play();
            DescreaseClipAmmo(validAmmo);
            projectile.SetInitialShotForce(CalculateSpread(direction, _angleShotSpread));
        }
    }

    protected override void OnAmmoClipZeroReach() { }

    protected override void OnAmmoZeroReach() { }

    protected override void OnEquip(Transform parent) { }

    protected override void OnFinishUsing() { }

    protected override void OnReload() { }

    protected override void OnUnequip() { }

    protected override void OnWhileReload(float currentDelayMS, float maxDelay) { }

    protected override void OnWhileUsing(float currentMS, float maxDelay)
    {
        Vector3 recoilNormal = transform.localPosition.GetNormalTo(_lastShotDirection);
        float t = (Mathf.Rad2Deg / 2 / maxDelay) * currentMS;

        transform.localPosition = FirstPersonOffset;
        transform.position += recoilNormal * Mathf.Sin(t) * 0.1f;
    }
}