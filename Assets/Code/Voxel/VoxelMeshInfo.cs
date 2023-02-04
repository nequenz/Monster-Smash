using UnityEngine;
using System;

public static class VoxelMeshInfo
{
    private const int SideVertexCount = 4;
    private const int SideTriangleCount = 6;

    private static readonly Vector3[] _voxelSideVerticies =
    {
        new Vector3(0,0,0),
        new Vector3(1,0,0),
        new Vector3(0,0,1),
        new Vector3(1,0,1),
    };
    private static readonly Vector3[] _voxelSideNormals =
    {
        Vector3.up,
        Vector3.up,
        Vector3.up,
        Vector3.up,
    };
    private static readonly int[] _voxelSideTriangles =
    {
        0,1,2,
        2,3,1
    };

    public static Vector3[] GetVertices()
    {
        Vector3[] result = new Vector3[SideVertexCount];
        _voxelSideTriangles.CopyTo(result, 0);

        return result;
    }

    public static Vector3[] GetNormals()
    {
        Vector3[] result = new Vector3[SideVertexCount];
        _voxelSideNormals.CopyTo(result, 0);

        return result;
    }

    public static int[] GetTriangles()
    {
        int[] result = new int[SideTriangleCount];
        _voxelSideTriangles.CopyTo(result, 0);

        return result;
    }
}