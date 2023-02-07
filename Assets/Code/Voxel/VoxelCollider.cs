﻿using UnityEngine;
using System;

public class VoxelCollider : IVoxelCollider
{
    private Transform _transform;
    private IVoxelData _data;
    private IVoxelMesh _mesh;


    public Transform AttachedTransform => _transform;
    public IVoxelData VoxelData => _data;
    public IVoxelMesh VoxelMesh => _mesh;


    public void SetTransform(Transform transform) => _transform = transform;

    public void SetVoxelData(IVoxelData data) => _data = data;

    public void SetVoxelMesh(IVoxelMesh mesh) => _mesh = mesh;

    public Vector3Int CalculateVoxelPosition(Vector3 position)
    {
        return default;
    }


}