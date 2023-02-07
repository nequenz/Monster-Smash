using UnityEngine;
using System;

[Serializable]
public class VoxelMesh : IVoxelMesh
{
    [SerializeReference] private float _sizeFactor = 1f;
    private Mesh _mesh;
    private IVoxelData _body;
    private float _rebuildDelay = 0.25f;
    private float _currentDelay = 0.0f;
    private bool _isDirty = false;


    public event Action Rebuilt;


    public IVoxelData AttachedVoxelBody => _body;
    public Mesh BuiltMesh => _mesh;
    public float SizeFactor => _sizeFactor;
    public bool IsDirty => _isDirty;
    public float RebuildDelay => _rebuildDelay;


    private void AddPlane(Vector3 position, Quaternion rotation)
    {
        float _sideCenter = -_sizeFactor / 2;
        Vector3 offset = new Vector3(_sideCenter, -_sideCenter, _sideCenter);
        Vector3 validSize = new Vector3(_body.Size.x, -_body.Size.y, _body.Size.z);
        Vector3[] vertices = new Vector3[VoxelMeshInfo.SideVertexCount];
        Vector3[] normals = new Vector3[VoxelMeshInfo.SideVertexCount];
        int[] triangles = new int[VoxelMeshInfo.SideTriangleCount];

        VoxelMeshInfo.SetVertices(ref vertices);
        VoxelMeshInfo.SetNormals(ref normals);
        VoxelMeshInfo.SetTriangles(ref triangles);

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = (rotation * (vertices[i] * _sizeFactor + offset))
                + (position * _sizeFactor) - offset
                - (validSize * _sizeFactor) / 2;
            normals[i] = rotation * normals[i];
        }

        for(int i = 0; i < triangles.Length; i ++)
            triangles[i] = triangles[i] + MeshAllocator.VertexCount;

        MeshAllocator.AddVertices(vertices);
        MeshAllocator.AddNormals(normals);
        MeshAllocator.AddTriangles(triangles);
    }

    public void SetVoxelBody(IVoxelData body)
    {
        _body = body;
        _isDirty = true;
    }

    public void SetSize(float size)
    {
        _sizeFactor = size;
        _isDirty = true;
    }

    public void SetRebuildDelay(float delay) => _rebuildDelay = delay;

    public void Update(float deltaTime)
    {
        //TO-DO
    }

    public void RebuildForced()
    {
        Vector3Int currentPosition = Vector3Int.zero;

        if (_mesh is null)
            _mesh = new();

        _mesh.Clear();
        MeshAllocator.Clear();

        for (int x = 0; x < _body.Size.x; x ++)
        {
            for (int z = 0; z < _body.Size.z; z++)
            {
                for (int y = 0; y < _body.Size.y; y++)
                {
                    currentPosition.Set(x, y, z);

                    if(_body.GetVoxel(currentPosition) != IVoxelData.EmptyVoxel)
                    {
                        if (_body.GetVoxel(x, y + 1, z) == IVoxelData.EmptyVoxel)
                            AddPlane(new Vector3Int(x, y, z), VoxelMeshInfo.TopSide);

                        if (_body.GetVoxel(x, y - 1, z) == IVoxelData.EmptyVoxel)
                            AddPlane(new Vector3Int(x, y, z), VoxelMeshInfo.BottomSide);

                        if (_body.GetVoxel(x + 1, y, z) == IVoxelData.EmptyVoxel)
                            AddPlane(new Vector3Int(x, y, z), VoxelMeshInfo.RightSide);

                        if (_body.GetVoxel(x - 1, y, z) == IVoxelData.EmptyVoxel)
                            AddPlane(new Vector3Int(x, y, z), VoxelMeshInfo.LeftSide);

                        if (_body.GetVoxel(x, y, z - 1) == IVoxelData.EmptyVoxel)
                            AddPlane(new Vector3Int(x, y, z), VoxelMeshInfo.FrontSide);

                        if (_body.GetVoxel(x, y, z + 1) == IVoxelData.EmptyVoxel)
                            AddPlane(new Vector3Int(x, y, z), VoxelMeshInfo.BackSide);
                    }
                }
            }
        }

        _mesh.vertices = MeshAllocator.CloneVertices();
        _mesh.normals = MeshAllocator.CloneNormals();
        _mesh.triangles = MeshAllocator.CloneTriangles();
        _mesh.Optimize();

        _isDirty = false;

        Rebuilt?.Invoke();
    }

}