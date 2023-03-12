using UnityEngine;
using System;


[Serializable]
public class ItemDispatcher : MonoBehaviour, IItemDispatcher
{
    [SerializeField] private PlayerCapture _playerCapture;
    private ActorLiving _lastItemOwner;
    private WeaponMechanics _lastWeaponMechanics;
    private ItemM4 _lastItemM4;
    private ItemRGD _lastItemRGD;


    public void Invoke()
    {
        ItemM4Using();
        ItemRGDUsing();
    }

    public void ItemM4Using()
    {
        Vector3 shootDirection;

        if (_lastItemM4 is null || _lastWeaponMechanics is null)
            return;

        shootDirection = _lastItemM4.transform.position.GetNormalTo(_playerCapture.AttachedCamera.GetAimPosition());

        _lastWeaponMechanics.SetShootDirection(shootDirection);

        _lastItemM4 = null;
    }

    public void ItemRGDUsing()
    {
        Vector3 shootDirection;

        if (_lastItemRGD is null || _lastWeaponMechanics is null)
            return;

        shootDirection = _lastItemRGD.transform.position.GetNormalTo(_playerCapture.AttachedCamera.GetAimPosition());

        _lastWeaponMechanics.SetShootDirection(shootDirection);

        _lastItemRGD = null;
    }

    public void DispatchItemOwner(ActorLiving actor)
    {
        _lastItemOwner = actor;
    }

    public void DispatchWeaponMechanics(WeaponMechanics weaponMechanics)
    {
        _lastWeaponMechanics = weaponMechanics;
    }

    public void Dispatch(ItemM4 itemM4)
    {
        _lastItemM4 = itemM4;
    }

    public void Dispatch(ItemRGD itemRGD)
    {
        _lastItemRGD = itemRGD;
    }
}