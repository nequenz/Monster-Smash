﻿using UnityEngine;
using System;

public class VoxelData : IVoxelData
{
    private bool _canBeReallocated = false;
    private byte[,,] _voxel;
    private Vector3 _size;


    public event Action<Vector3> Changed;


    public bool CanBeReallocated => _canBeReallocated;
    public Vector3 Size => _size;


    private bool IsAccessValid(Vector3 position)
    {
        bool zeroCheck = position.x >= 0 && position.y >= 0 && position.z >= 0;
        bool sizeCheck = position.x < _size.x && position.y < _size.y && position.z < _size.z;

        return zeroCheck && sizeCheck;
    }

    public void Allocate(Vector3Int size)
    {
        if(size.x < 0 || size.y < 0 || size.z < 0)
            throw new ArgumentOutOfRangeException("Wrong params of allocation size");

        if (_canBeReallocated == false && _voxel is not null)
            return;

        _voxel = new byte[size.x, size.y, size.z];
        _size = size;
    }

    public void SetVoxel(Vector3Int position, int value)
    {
        if (IsAccessValid(position))
            _voxel[position.x, position.y, position.z] = (byte)value;
    }

    public int GetVoxel(Vector3Int position)
    {
        if (IsAccessValid(position))
            return _voxel[position.x, position.y, position.z];

        return IVoxelData.EmptyVoxel;
    }

    public int GetVoxel(int x, int y, int z) => GetVoxel(new Vector3Int(x, y, z));
}