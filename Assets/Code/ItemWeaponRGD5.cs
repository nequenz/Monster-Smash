using UnityEngine;


public sealed class ItemWeaponRGD5 : ItemWeapon
{
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

            DescreaseClipAmmo(validAmmo);
            projectile.SetInitialShotForce(direction);
        }
    }

    protected override void OnAmmoClipZeroReach() { }

    protected override void OnAmmoZeroReach()
    {
     
    }

    protected override void OnEquip(Transform parent) { }

    protected override void OnFinishUsing() { }

    protected override void OnReload() { }

    protected override void OnUnequip() { }

    protected override void OnWhileReload(float currentDelayMS, float maxDelay) { }

    protected override void OnWhileUsing(float currentMS, float maxDelay) { }
}