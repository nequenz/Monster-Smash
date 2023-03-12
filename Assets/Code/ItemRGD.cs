using UnityEngine;


public sealed class ItemRGD : Item
{
    [SerializeReference, SubclassSelector] private WeaponMechanics _weaponMechanics = new();
    private Vector3 _shootDirection;


    protected override void OnWhileUsing(float currentDelay, float maxDelay)
    {
        
    }

    public override void AcceptDispatcher(IItemDispatcher itemDispatcher)
    {
        itemDispatcher.DispatchWeaponMechanics(_weaponMechanics);
        itemDispatcher.Dispatch(this);
    }

    public override void Use()
    {
        if (IsReadyToUse() == false)
            return;

        ProjectileBasic proj = _weaponMechanics.Shoot();

        StartUsingTimer();
    }
}