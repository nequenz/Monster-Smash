using UnityEngine;
using System;

public interface IVoxelData
{
    public event Action<Vector3> Collided;
    public event Action<Vector3> Changed;


    public bool CanBeReallocated { get; }
    public Vector3 Size { get; }


    public void Allocate(Vector3Int size);

    public void SetVoxel(Vector3Int position, byte value);

    public byte GetVoxel(Vector3Int position);
}