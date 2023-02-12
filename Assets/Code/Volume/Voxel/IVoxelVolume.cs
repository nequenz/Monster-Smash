using UnityEngine;
using System;

public interface IVoxelVolume : IVolume<bool>
{
    public const bool Empty = false;
    public const bool Full = true;


    public event Action Rebuilt;


    public IVolumeReadOnly<Color> PrefabToBuild { get; }


    public void SetVolumePrefabToBuild(IVolumeReadOnly<Color> prefabToBuild);

    public void Rebuild();
}
