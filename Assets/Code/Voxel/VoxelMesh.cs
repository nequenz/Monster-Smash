using UnityEngine;
using System;


public class VoxelMesh : IVoxelMesh
{
    private Mesh _mesh;
    private IVoxelData _body;
    private float _sizeFactor = 0.25f;
    private bool _isDirty = false;
    private float _rebuildDelay = 0.25f;
    private Vector3[] _voxelSideVerticies = 
    {
        new Vector3(0,0,0),
        new Vector3(1,0,0),
        new Vector3(0,0,1),
        new Vector3(1,0,1),
    };
    private Vector3[] _voxelSideNormals =
    {
        Vector3.up,
        Vector3.up,
        Vector3.up,
        Vector3.up,
    };
    private int[] _voxelSideTriangles =
    {
        0,1,2,
        2,3,1
    };


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

        for(int i = 0; i < _voxelSideVerticies.Length; i++)
        {
            _voxelSideVerticies[i] = rotation * (_voxelSideVerticies[i] * _sizeFactor + offset);
            _voxelSideNormals[i] = rotation * _voxelSideNormals[i];
        }
            
        _mesh.vertices = _voxelSideVerticies;
        _mesh.triangles = _voxelSideTriangles;
        _mesh.normals = _voxelSideNormals;
    }

    public void CreateNewMesh()
    {
        _mesh = new Mesh();
    }

    public void SetVoxelBody(IVoxelData body)
    {
        _body = body;
        _isDirty = true;
        Changed?.Invoke();
    }

    public void SetSize(float size) => _sizeFactor = size;

    public void Rebuild()
    {

        _isDirty = false;
        Rebuilt?.Invoke();
    }

}