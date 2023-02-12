using UnityEngine;
using System;


public interface IVoxelMesh
{
    public event Action Rebuilt;


    public IVolumeReadOnly<Color> AttachedColorVolume { get; }
    public IVoxelVolume AttachedVoxelVolume { get; }
    public Mesh BuiltMesh { get; }
    public float SizeFactor { get; }
    public bool IsDirty { get; }
    public float RebuildDelay { get; }


    public void SetVoxelVolume(IVoxelVolume voxels);

    public void SetColorVolume(IVolumeReadOnly<Color> colors);

    public void SetRebuildDelay(float delay);

    public void RebuildForced();

    public void SetSize(float size);

    public void Update(float deltaTime);
}
