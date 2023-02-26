using UnityEngine;

public class ProjectileBullet : ProjectileBasic
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void FixedUpdate()
    {
        RaycastHit hit = Raycast(2f);

        if(hit.collider is not null && hit.collider.Is(out TestObject body))
        {
            body.AttachedVoxelBody.SetVoxel(hit.point,IVoxelVolume.Empty);
            Destroy(gameObject);
        }
    }

    protected override void OnLifeTimeZeroReach()
    {
        Destroy(gameObject);
    }
}