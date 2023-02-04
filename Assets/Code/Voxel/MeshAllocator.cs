using UnityEngine;
using System;
using System.Collections.Generic;

public static class MeshAllocator
{
    private static List<Vector3> _verticies = new();
    private static List<Vector3> _normals = new();
    private static List<Vector2> _uvs = new();
    private static List<int> _triangles = new();
    private static int _vertexIndex = 0;
    private static int _normalIndex = 0;
    private static int _uvIndex = 0;
    private static int _triangleIndex = 0;

    public static void NewBuild()
    {
        _vertexIndex = 0;
        _normalIndex = 0;
        _uvIndex = 0;
        _triangleIndex = 0;

        _verticies.Clear();
        _normals.Clear();
        _uvs.Clear();
        _triangles.Clear();
    }

    public static void AddVertex(Vector3 vertex) => _verticies.Add(vertex);

    public static void AddVerticies(Vector3[] verticies) => _verticies.AddRange(verticies);

    public static void AddNormal(Vector3 normal) => _normals.Add(normal);

    public static void AddNormals(Vector3[] normals) => _normals.AddRange(normals);

    public static void AddUV(Vector2 uv) => _uvs.Add(uv);

    public static void AddUVs(Vector2[] uvs) => _uvs.AddRange(uvs);

    public static void AddTriangle(int t1, int t2, int t3)
    {
        _triangles.Add(t1);
        _triangles.Add(t2);
        _triangles.Add(t3);
    }

    public static void AddTriangles(int[] triangles) => _triangles.AddRange(triangles);

    public static Vector3[] GetVertices() => _verticies.ToArray();

    public static Vector3[] GetNormals() => _normals.ToArray();

    public static Vector2[] GetUVs() => _uvs.ToArray();

    public static int[] GetTriangles() => _triangles.ToArray();
}