using UnityEngine;


public interface IVoxelTransform
{
    public Transform AttachedTransform { get; }
    public IVoxelVolume AttachedVoxelVolume { get; }
    public IVoxelMesh AttachedVoxelMesh { get; }
    public BoxCollider AttachedBoxCollider { get; }


    public IVoxelTransform Init(Transform transform, IVoxelVolume volume, IVoxelMesh mesh, BoxCollider collider);

    public void SetTransform(Transform transform);

    public void SetVolume(IVoxelVolume volume);

    public void SetVolumeMesh(IVoxelMesh mesh);

    public void SetBoxCollider(BoxCollider collider);

    public Vector3Int CalculateVoxelPosition(Vector3 position);
}