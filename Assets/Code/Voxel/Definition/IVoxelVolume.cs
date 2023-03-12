using UnityEngine;
using System;

public interface IVoxelVolume : IVolume<bool>
{
    public const bool Empty = false;
    public const bool Full = true;


    public event Action Matched;


    public IVolumeReadOnly<Color> ModelToBuild { get; }

    public IVoxelVolume Init(IVolumeReadOnly<Color> prefabToBuild, bool canBeReallocated);

    public void SetVolumeModelToBuild(IVolumeReadOnly<Color> prefabToBuild);

    public void MatchToModel();
}
