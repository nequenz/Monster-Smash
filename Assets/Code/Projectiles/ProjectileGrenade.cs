using UnityEngine;


public sealed class ProjectileGrenade : ProjectileBasic
{
    [SerializeField] private Explosion _explosion;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnLifeTimeZeroReach()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}