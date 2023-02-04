using UnityEngine;
using System;

public class VoxelMesh : IVoxelMesh
{
    private Mesh _mesh;
    private IVoxelData _body;
    private float _sizeFactor = 0.25f;
    private bool _isDirty = false;
    private float _rebuildDelay = 0.25f;


    public event Action Changed;
    public event Action Rebuilt;


    public IVoxelData AttachedVoxelBody => _body;
    public Mesh BuiltMesh => _mesh;
    public float SizeFactor => _sizeFactor;
    public bool IsDirty => _isDirty;
    public float RebuildDelay => _rebuildDelay;


    private void AddPlane(Vector3 position, Quaternion rotation)
    {
        float _sideCenter = -_sizeFactor / 2;
        Vector3 offset = new Vector3(_sideCenter, 0, _sideCenter);
        Vector3[] verticies = VoxelMeshInfo.GetVertices();
        Vector3[] normals = VoxelMeshInfo.GetNormals();
        int[] triangles = VoxelMeshInfo.GetTriangles();

        for (int i = 0; i < verticies.Length; i++)
        {
            verticies[i] = rotation * (verticies[i] * _sizeFactor + offset);
            normals[i] = rotation * normals[i];
        }

        MeshAllocator.AddVerticies(verticies);
        MeshAllocator.AddNormals(normals);
        MeshAllocator.AddTriangles(triangles);
    }

    public void CreateNewMesh()
    {
        _mesh = new Mesh();
        _isDirty = true;
    }

    public void SetVoxelBody(IVoxelData body)
    {
        _body = body;
        _isDirty = true;
        Changed?.Invoke();
    }

    public void SetSize(float size)
    {
        _sizeFactor = size;
        _isDirty = true;
    }

    public void Rebuild()
    {
        MeshAllocator.NewBuild();

        for(int i = 0; i < _body.Size.x; i ++)
        {
            for (int k = 0; k < _body.Size.z; k++)
            {
                for (int j = 0; j < _body.Size.y; j++)
                {

                }
            }
        }

        _isDirty = false;
        Rebuilt?.Invoke();
    }

}