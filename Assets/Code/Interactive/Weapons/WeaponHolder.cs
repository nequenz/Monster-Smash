using System;
using UnityEngine;


public class WeaponHolder
{
    private Weapon _equippedWeapon;


    public void SetWeapon(Weapon weapon)
    {
        _equippedWeapon = weapon;
    }

    public void Update(float deltaTime)
    {
        _equippedWeapon.Update(deltaTime);
    }

    public void Shoot(Vector3 direction)
    {
        _equippedWeapon.Shoot(direction);
    }
}