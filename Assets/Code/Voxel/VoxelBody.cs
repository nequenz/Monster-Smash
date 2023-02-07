using UnityEngine;


[RequireComponent(typeof(MeshCollider))]
public class VoxelBody : MonoBehaviour
{
    [SerializeReference] private IVoxelMesh _voxelMesh = new VoxelMesh();
    [SerializeReference] private IVoxelData _voxels = new VoxelData();
    [SerializeReference] private IVoxelTransform _voxelTransform = new VoxelTransform();
    [SerializeField] private MeshFilter _filter;
    private MeshCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<MeshCollider>();

        _voxels.Allocate();
        _voxels.Changed += OnVoxelsChanged;
        _voxelMesh.SetVoxelBody(_voxels);
        _voxelMesh.RebuildForced();
        _voxelMesh.Rebuilt += OnVoxelMeshRebuild;
        _voxelTransform.SetVoxelMesh(_voxelMesh);
        _voxelTransform.SetVoxelData(_voxels);

        _collider.transform.localScale = _voxels.Size * _voxelMesh.SizeFactor;
        _filter.mesh = _voxelMesh.BuiltMesh;
    }

    private void OnVoxelsChanged(Vector3Int position)
    {
        _voxelMesh.RebuildForced();
    }

    private void OnVoxelMeshRebuild()
    {

    }
}