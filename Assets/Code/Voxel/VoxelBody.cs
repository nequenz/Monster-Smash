using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class VoxelBody : MonoBehaviour
{
    [SerializeReference] private IVoxelMesh _voxelMesh = new VoxelMesh();
    [SerializeReference] private IVoxelData _voxels = new VoxelData();
    [SerializeReference] private IVoxelTransform _voxelTransform = new VoxelTransform();
    [SerializeField] private MeshFilter _meshFilter;
    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();

        _voxels.Allocate();
        _voxels.Changed += OnVoxelsChanged;
        _voxelMesh.SetVoxelBody(_voxels);
        _voxelMesh.RebuildForced();
        _voxelMesh.Rebuilt += OnVoxelMeshRebuild;
        _voxelTransform.SetVoxelMesh(_voxelMesh);
        _voxelTransform.SetVoxelData(_voxels);

        _collider.transform.localScale = _voxels.Size * _voxelMesh.SizeFactor;
        _meshFilter.mesh = _voxelMesh.BuiltMesh;
    }

    private void Update()
    {
        _voxelTransform.AttachedTransform.rotation *= Quaternion.AngleAxis(0.1f, Vector3.up);
    }

    private void OnVoxelsChanged(Vector3Int position)
    {
        _voxelMesh.RebuildForced();
    }

    private void OnVoxelMeshRebuild()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        _voxels.SetVoxel(_voxelTransform.CalculateVoxelPosition(other.transform.position), 0);
    }
}