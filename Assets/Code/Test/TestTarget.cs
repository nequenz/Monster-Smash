using UnityEngine;


public class TestTarget : MonoBehaviour
{
    private MeshFilter _filter;
    private IVoxelData _voxels = new VoxelData();
    private VoxelMesh _voxelMesh = new VoxelMesh();

    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();

        _voxelMesh.CreateNewMesh();

        _voxelMesh.AddPlane(new Vector3Int(0, 0, 0), VoxelMeshInfo.TopSide);
        _voxelMesh.AddPlane(new Vector3Int(0, 0, 0), VoxelMeshInfo.BottomSide);
        _voxelMesh.AddPlane(new Vector3Int(0, 0, 0), VoxelMeshInfo.RightSide);
        _voxelMesh.AddPlane(new Vector3Int(0, 0, 0), VoxelMeshInfo.LeftSide);
        _voxelMesh.AddPlane(new Vector3Int(0, 0, 0), VoxelMeshInfo.FrontSide);
        _voxelMesh.AddPlane(new Vector3Int(0, 0, 0), VoxelMeshInfo.BackSide);

        _voxelMesh.BuiltMesh.vertices = MeshAllocator.CloneVertices();
        _voxelMesh.BuiltMesh.normals = MeshAllocator.CloneNormals();
        _voxelMesh.BuiltMesh.triangles = MeshAllocator.CloneTriangles();
        _filter.mesh = _voxelMesh.BuiltMesh;

    }
}