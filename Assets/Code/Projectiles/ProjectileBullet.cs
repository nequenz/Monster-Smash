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
        Vector3 validContactPoint;

        if(hit.collider is not null && hit.collider.Is(out ActorVoxelBody body))
        {
            validContactPoint = transform.position.GetNormalTo(hit.point) * 0.1f;

            body.AttachedVoxelBody.SetVoxel(hit.point + validContactPoint, IVoxelVolume.Empty);
            Destroy(gameObject);
        }
    }

    protected override void OnLifeTimeZeroReach()
    {
        Destroy(gameObject);
    }
}