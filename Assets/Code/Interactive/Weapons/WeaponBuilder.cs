using System;
using UnityEngine;

[Serializable]
public class WeaponBuilder
{
    [SerializeField] Projectile _selectedProjectile;
    [SerializeField] Transform _shotPoint;


    public Transform ShotPoint => _shotPoint;


    public Weapon CreateTestWeapon()
    {
        Weapon weapon = new();

        weapon.Init(_shotPoint, _selectedProjectile,0.25f, 100);

        return weapon;
    }
}