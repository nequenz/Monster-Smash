using UnityEngine;


public interface IVoxelTransform
{
    public Transform AttachedTransform { get; }
    public IVoxelVolume AttachedVoxelVolume { get; }
    public IVoxelMesh AttachedVoxelMesh { get; }


    public void SetTransform(Transform transform);

    public void SetVolume(IVoxelVolume volume);

    public void SetVolumeMesh(IVoxelMesh mesh);

    public Vector3Int CalculateVoxelPosition(Vector3 position);
}