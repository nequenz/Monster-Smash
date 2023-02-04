﻿using UnityEngine;
using System;


public interface IVoxelMesh
{
    public event Action Changed;
    public event Action Rebuilt;


    public IVoxelData AttachedVoxelBody { get; }
    public Mesh BuiltMesh { get; }
    public float SizeFactor { get; }
    public bool IsDirty { get; }
    public float RebuildDelay { get; }


    public void CreateNewMesh();

    public void SetVoxelBody(IVoxelData body);

    public void Rebuild();

    public void SetSize(float size);
}