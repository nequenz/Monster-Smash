using UnityEngine;
using System;


public interface IVoxelTransform
{
    public Transform AttachedTransform { get; }
    public IVoxelData VoxelData { get; }
    public IVoxelMesh VoxelMesh { get; }


    public void SetTransform(Transform transform);

    public void SetVoxelData(IVoxelData data);

    public void SetVoxelMesh(IVoxelMesh mesh);

    public Vector3Int CalculateVoxelPosition(Vector3 position);
}