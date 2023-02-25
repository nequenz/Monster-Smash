using UnityEngine;


public sealed class WeaponM4 : WeaponBasic
{
    [SerializeField] private ParticleSystem _afterShotEffect;
    private Vector3 _lastShotDirection;


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
            _lastShotDirection = direction;

            _afterShotEffect.Play();
            DescreaseClipAmmo(validAmmo);
            projectile.SetShotForce(CalculateSpread(direction, new Vector3(5, 5, 5)));
        }

    }

    protected override void OnWhileAfterShoot(float currentDelayMS, float maxDelay)
    {
        Vector3 recoilNormal = transform.localPosition.GetNormalTo(_lastShotDirection);
        float t = (Mathf.Rad2Deg / 2 / maxDelay) * currentDelayMS;

        transform.localPosition = FirstPersonViewOffset;
        transform.position += recoilNormal * Mathf.Sin(t) * 0.1f;
    }

    protected override void OnReload() { }

    protected override void OnWhileReload(float currentDelayMS, float maxDelay) { }

    protected override void OnAmmoZeroReach() { }

    protected override void OnAmmoClipZeroReach() { }

}