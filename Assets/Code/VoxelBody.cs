using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider))]
public class VoxelBody : MonoBehaviour
{
    [SerializeReference] private IVoxelVolume _voxels;
    [SerializeReference] private IVoxelMesh _mesh;
    [SerializeReference] private IVoxelTransform _transform;
    [SerializeField] private MeshFilter _meshFilter;
    private IVolumeReadOnly<Color> _colorPrefab;
    private BoxCollider _collider;


    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();

        //_voxels.SetVolumePrefabToBuild( ColorVolumeReader.LoadPrefab );
        _voxels.Allocate();
        _voxels.Changed += OnVoxelsChanged;
        _mesh.SetVoxelVolume(_voxels);
        _mesh.SetColorVolume(_colorPrefab);
        _mesh.RebuildForced();
        _mesh.Rebuilt += OnMeshRebuild;

        _collider.transform.localScale = _voxels.Size * _mesh.FaceSize;
        _meshFilter.mesh = _mesh.BuiltMesh;
    }

    private void OnVoxelsChanged(Vector3Int position)
    {

    }

    private void OnMeshRebuild()
    {

    }
}