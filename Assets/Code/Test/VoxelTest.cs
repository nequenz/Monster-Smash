using UnityEngine;


public class VoxelTest : MonoBehaviour
{
    [SerializeReference] private IVoxelMesh _voxelMesh = new VoxelMesh();
    [SerializeReference] private IVoxelData _voxels = new VoxelData();
    private MeshFilter _filter;
    private Collider _collider;


    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();
        _collider = GetComponentInChildren<Collider>();

        _voxels.Allocate();
        _voxelMesh.SetVoxelBody(_voxels);
        _voxelMesh.RebuildForced();

        _collider.transform.localScale = _voxels.Size * _voxelMesh.SizeFactor;
        _filter.mesh = _voxelMesh.BuiltMesh;
    }

    private void Update()
    {
        _voxelMesh.Update(Time.deltaTime);
        transform.rotation *= Quaternion.AngleAxis(0.1f,Vector3.up);
    }
}