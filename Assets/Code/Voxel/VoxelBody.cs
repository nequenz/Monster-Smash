using UnityEngine;
using System;

public class VoxelBody : IVoxelBody
{
    private IVoxelBodyDestroyPolicy _policy;
    private bool _canBeReallocated = false;
    private byte[,,] _voxel;

    public event Action<Vector3> Collided;
    public event Action<Vector3> Changed;


    public IVoxelBodyDestroyPolicy DestroyPolicy => _policy;
    public bool CanBeReallocated => _canBeReallocated;

    public void Allocate(Vector3Int size)
    {
        if(size.x < 0 || size.y < 0 || size.z < 0)
            throw new ArgumentOutOfRangeException("Wrong params of allocation size");

        _voxel = new byte[size.x, size.y, size.z];
    }

    public void SetDestroyPolicy(IVoxelBodyDestroyPolicy policy)
    {
        //TO-DO
    }

    public void GetVoxel(Vector3Int position)
    {
        //TO-DO
    }

    public void SetVoxel(Vector3Int position, byte value)
    {
        //TO-DO
    }
}