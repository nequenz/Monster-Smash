using UnityEngine;
using System;

public interface IVoxelBody
{
    public event Action<Vector3> Collided;
    public event Action<Vector3> Changed;


    public bool CanBeReallocated { get; }

    public void Allocate(Vector3Int size);

    public void SetVoxel(Vector3Int position, byte value);

    public void GetVoxel(Vector3Int position);
}