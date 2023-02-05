using UnityEngine;
using System;


public class VoxelMesh : IVoxelMesh
{
    private Mesh _mesh;
    private IVoxelData _body;
    private float _sizeFactor = 1f;
    private bool _isDirty = false;
    private float _rebuildDelay = 0.25f;


    public event Action Changed;
    public event Action Rebuilt;


    public IVoxelData AttachedVoxelBody => _body;
    public Mesh BuiltMesh => _mesh;
    public float SizeFactor => _sizeFactor;
    public bool IsDirty => _isDirty;
    public float RebuildDelay => _rebuildDelay;


    public void AddPlane(Vector3 position, Quaternion rotation)
    {
        float _sideCenter = -_sizeFactor / 2;
        Vector3 offset = new Vector3(_sideCenter, -_sideCenter, _sideCenter);
        Vector3[] vertices = new Vector3[VoxelMeshInfo.SideVertexCount];
        Vector3[] normals = new Vector3[VoxelMeshInfo.SideVertexCount];
        int[] triangles = new int[VoxelMeshInfo.SideTriangleCount];

        VoxelMeshInfo.SetVertices(ref vertices);
        VoxelMeshInfo.SetNormals(ref normals);
        VoxelMeshInfo.SetTriangles(ref triangles);

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = rotation * (vertices[i] * _sizeFactor + offset) + position * _sizeFactor;
            normals[i] = rotation * normals[i];
        }

        for(int i = 0; i < triangles.Length; i ++)
            triangles[i] = triangles[i] + MeshAllocator.VertexCount;

        MeshAllocator.AddVertices(vertices);
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
        Vector3Int voxelPosition = Vector3Int.zero;
        Vector3Int currentPosition = Vector3Int.zero;

        for (int i = 0; i < _body.Size.x; i ++)
        {
            for (int k = 0; k < _body.Size.z; k++)
            {
                for (int j = 0; j < _body.Size.y; j++)
                {
                    currentPosition.Set(i, j, k);
                    voxelPosition.x = i;
                    voxelPosition.z = k;
                    voxelPosition.y = j - 1;

                    if(_body.GetVoxel(voxelPosition) == IVoxelData.EmptyVoxel)
                    {
                        //AddPlane(currentPosition, );
                    }
                }
            }
        }

        _isDirty = false;
        Rebuilt?.Invoke();
    }

}