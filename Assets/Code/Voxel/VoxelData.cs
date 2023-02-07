using UnityEngine;
using System;


[Serializable]
public class VoxelData : IVoxelData
{
    [SerializeField] private Vector3Int _inputSize;
    [SerializeField] private int _defautlFillValue = IVoxelData.EmptyVoxel;
    [SerializeField] private bool _canBeReallocated = false;
    private Vector3Int _size;
    private byte[,,] _voxels;
    

    public event Action<Vector3Int> Changed;


    public bool CanBeReallocated => _canBeReallocated;
    public Vector3 Size => _size;


    private bool IsAccessValid(Vector3 position)
    {
        bool zeroCheck = position.x >= 0 && position.y >= 0 && position.z >= 0;
        bool sizeCheck = position.x < _size.x && position.y < _size.y && position.z < _size.z;

        return zeroCheck && sizeCheck;
    }

    public void Allocate()
    {
        Allocate(_inputSize, _defautlFillValue);
    }

    public void Allocate(Vector3Int size, int setValue = IVoxelData.EmptyVoxel)
    {
        if(size.x < 0 || size.y < 0 || size.z < 0)
            throw new ArgumentOutOfRangeException("Wrong params of allocation size");

        if (_canBeReallocated == false && _voxels is not null)
            return;

        _voxels = new byte[size.x, size.y, size.z];
        _size = size;

        if(setValue != IVoxelData.EmptyVoxel)
        {
            for(int x = 0; x < size.x; x ++)
            {
                for (int z = 0; z < size.z; z++)
                {
                    for (int y = 0; y < size.y; y++)
                    {
                        _voxels[x, y, z] = (byte)setValue;
                    }
                }
            }
        }
    }

    public void SetVoxel(Vector3Int position, int value)
    {
        if (IsAccessValid(position))
        {
            _voxels[position.x, position.y, position.z] = (byte)value;

            Changed?.Invoke(position);
        }  
    }

    public int GetVoxel(Vector3Int position)
    {
        if (IsAccessValid(position))
            return _voxels[position.x, position.y, position.z];

        return IVoxelData.EmptyVoxel;
    }

    public int GetVoxel(int x, int y, int z) => GetVoxel(new Vector3Int(x, y, z));
}