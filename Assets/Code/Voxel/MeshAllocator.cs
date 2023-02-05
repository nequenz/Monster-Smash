using UnityEngine;
using System;
using System.Collections.Generic;

public static class MeshAllocator
{
    private static List<Vector3> _verticies = new();
    private static List<Vector3> _normals = new();
    private static List<Vector2> _uvs = new();
    private static List<int> _triangles = new();

    public static int VertexCount => _verticies.Count;
    public static int NormalCount => _normals.Count;
    public static int UVCount => _uvs.Count;
    public static int TriangleCount => _triangles.Count;


    public static void Clear()
    {
        _verticies.Clear();
        _normals.Clear();
        _uvs.Clear();
        _triangles.Clear();
    }

    public static void AddVertices(Vector3[] other) => _verticies.AddRange(other);

    public static void AddNormals(Vector3[] other) => _normals.AddRange(other);

    public static void AddUVs(Vector2[] other) => _uvs.AddRange(other);

    public static void AddTriangles(int[] other) => _triangles.AddRange(other);

    public static Vector3[] CloneVertices() => _verticies.ToArray();

    public static Vector3[] CloneNormals() => _normals.ToArray();

    public static Vector2[] CloneUVs() => _uvs.ToArray();

    public static int[] CloneTriangles() => _triangles.ToArray();
}