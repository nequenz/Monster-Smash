using System;
using System.Collections;
using UnityEngine;


[Serializable]
public class VoxelVolume : IVoxelVolume
{
    [SerializeField] private bool _canBeReallocated = false;
    private Vector3Int _size;
    private BitArray[,] _bits;
    private IVolumeReadOnly<Color> _modelToBuild;
    private bool _isChangeEventEnabled = true;


    public event Action<Vector3Int> Changed;
    public event Action Matched;


    public IVolumeReadOnly<Color> ModelToBuild => _modelToBuild;
    public bool UndefinedValue => false;
    public bool CanBeReallocated => _canBeReallocated;
    public Vector3 Size => _size;


    private bool IsAccessValid(Vector3 position)
    {
        bool zeroCheck = position.x >= 0 && position.y >= 0 && position.z >= 0;
        bool sizeCheck = position.x < _size.x && position.y < _size.y && position.z < _size.z;

        return zeroCheck && sizeCheck;
    }

    public IVoxelVolume Init(IVolumeReadOnly<Color> colorPrefab, bool canBeReallocated)
    {
        SetVolumeModelToBuild(colorPrefab);
        _canBeReallocated = canBeReallocated;

        return this;
    }

    public void Allocate()
    {
        Vector3Int size = default;

        if(_modelToBuild is not null)
        {
            size.x = (int)_modelToBuild.Size.x;
            size.y = (int)_modelToBuild.Size.y;
            size.z = (int)_modelToBuild.Size.z;

            Allocate(size, IVoxelVolume.Empty);
        }
    }

    public void Allocate(Vector3Int size, bool initValue)
    {
        if (size.x < 0 || size.y < 0 || size.z < 0)
            throw new ArgumentOutOfRangeException("Wrong params of allocation size");

        if (_canBeReallocated == false && _bits is not null)
            return;

        _bits = new BitArray[size.x, size.z];
        _size = size;

        for(int x = 0; x < size.x; x ++)
        {
            for (int z = 0; z < size.z; z++)
            {
                _bits[x, z] = new BitArray(size.y);
            }
        }
    }

    public void MatchToModel()
    {
        if (_modelToBuild is null)
            return;

        _isChangeEventEnabled = false;

        for(int x = 0; x < _size.x; x ++)
        {
            for (int z = 0; z < _size.z; z++)
            {
                for (int y = 0; y < _size.y; y++)
                {
                    if (_modelToBuild.GetValue(x, y, z) != _modelToBuild.UndefinedValue)
                        SetValue(new Vector3Int(x, y, z), IVoxelVolume.Full);
                }
            }
        }

        _isChangeEventEnabled = true;

        Matched?.Invoke();
    }

    public void SetValue(Vector3Int position, bool value)
    {
        bool prevValue;

        if (IsAccessValid(position))
        {
            prevValue = _bits[position.x, position.z].Get(position.y);

            if(prevValue != value)
            {
                _bits[position.x, position.z].Set(position.y, value);

                if(_isChangeEventEnabled)
                    Changed?.Invoke(position);
            }
        }
    }

    public void SetVolumeModelToBuild(IVolumeReadOnly<Color> colorPrefab) => _modelToBuild = colorPrefab;

    public bool GetValue(Vector3Int position)
    {
        if (IsAccessValid(position))
            return _bits[position.x, position.z].Get(position.y);

        return UndefinedValue;
    }

    public bool GetValue(int x, int y, int z) => GetValue(new Vector3Int(x, y, z));

}
