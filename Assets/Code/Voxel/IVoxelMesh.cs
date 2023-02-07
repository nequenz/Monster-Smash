using UnityEngine;
using System;


public interface IVoxelMesh
{
    public event Action Rebuilt;


    public IVoxelData AttachedVoxelBody { get; }
    public Mesh BuiltMesh { get; }
    public float SizeFactor { get; }
    public bool IsDirty { get; }
    public float RebuildDelay { get; }


    public void SetVoxelBody(IVoxelData body);

    public void SetRebuildDelay(float delay);

    public void RebuildForced();

    public void SetSize(float size);

    public void Update(float deltaTime);
}