using UnityEngine;


public sealed class ItemM4 : Item
{
    [SerializeReference, SubclassSelector] private WeaponMechanics _weaponMechanics = new();
    [SerializeField] private ParticleSystem _afterShotVFX;
    private Vector3 _directionToShoot;


    protected override void OnWhileUsing(float currentDelay, float maxDelay)
    {
        const float DragFactor = 0.1f;

        float t = (Mathf.Rad2Deg / 2 / maxDelay) * currentDelay;

        transform.localPosition = FirstPersonOffset;
        transform.position += _directionToShoot * Mathf.Sin(t) * DragFactor;
    }

    public override void Use()
    {
        if (IsReadyToUse() == false)
            return;

        _afterShotVFX.Play();
        _weaponMechanics.Shoot();
        StartUsingTimer();
    }

    public override void AcceptDispatcher(IItemDispatcher itemDispatcher)
    {
        itemDispatcher.DispatchWeaponMechanics(_weaponMechanics);
        itemDispatcher.Dispatch(this);
    }
}