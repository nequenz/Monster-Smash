using UnityEngine;


public class VoxelTest : MonoBehaviour
{
    private MeshFilter _filter;
    private IVoxelData _voxels = new VoxelData();
    private IVoxelMesh _voxelMesh = new VoxelMesh();

    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();

        _voxels.Allocate(new Vector3Int(2, 1, 2));
        _voxels.SetVoxel(new Vector3Int(0, 0, 0), 1);
        _voxels.SetVoxel(new Vector3Int(1, 0, 1), 1);
        _voxelMesh.SetVoxelBody(_voxels);
        _voxelMesh.RebuildForced();

        _filter.mesh = _voxelMesh.BuiltMesh;
    }

    private void Update()
    {
        //_voxelMesh.Update(Time.deltaTime);
        //transform.rotation *= Quaternion.AngleAxis(1,Vector3.up);
    }
}