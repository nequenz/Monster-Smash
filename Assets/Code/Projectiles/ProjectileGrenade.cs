using UnityEngine;


public sealed class ProjectileGrenade : ProjectileBasic
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnLifeTimeZeroReach()
    {
        Destroy(gameObject);
    }
}