using UnityEngine;

public sealed class WeaponGrenade : Weapon
{

    protected override void OnAwake()
    {

    }

    protected override void OnUpdate()
    {

    }

    protected override void OnShoot(Vector3 direction)
    {
        int validAmmo = CalculateAvailableClipAmmo(1);

        if (validAmmo == 1)
        {
            ProjectileBasic projectile = CreateProjectile(MainShootMain.position);

            DescreaseClipAmmo(validAmmo);
            projectile.SetShotForce(direction);
        }
    }

    protected override void OnAmmoClipZeroReach() { }

    protected override void OnAmmoZeroReach() { }

    protected override void OnReload() { }

    protected override void OnWhileAfterShoot(float currentDelayMS, float maxDelay) { }

    protected override void OnWhileReload(float currentDelayMS, float maxDelay) { }
}

