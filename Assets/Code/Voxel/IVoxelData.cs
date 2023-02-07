using UnityEngine;
using System;

public interface IVoxelData
{
    public const int EmptyVoxel = 0x0;


    public event Action<Vector3> Changed;


    public bool CanBeReallocated { get; }
    public Vector3 Size { get; }


    public void Allocate();

    public void Allocate(Vector3Int size, int setValue);

    public void SetVoxel(Vector3Int position, int value);

    public int GetVoxel(Vector3Int position);

    public int GetVoxel(int x, int y, int z);
}