using UnityEngine;


public class VoxelTest : MonoBehaviour
{
    private MeshFilter _filter;
    private IVoxelData _voxels = new VoxelData();
    private IVoxelMesh _voxelMesh = new VoxelMesh();

    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();

        _voxelMesh.SetVoxelBody(_voxels);
        _voxels.Allocate(new Vector3Int(2, 2, 2));
        _voxels.SetVoxel(new Vector3Int(1, 1, 1), 0);
        _voxels.SetVoxel(new Vector3Int(0, 0, 1), 0);

        _filter.mesh = _voxelMesh.BuiltMesh;
    }
}