using UnityEngine;

public class ProjectileBullet : ProjectileBasic
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        RaycastHit hit = Raycast(2f);

        if(hit.collider is not null && hit.collider.Is(out TestObject body))
        {
            body.AttachedVoxelBody.SetVoxel(hit.point,IVoxelVolume.Empty);
            Destroy(gameObject);
        }
    }
}