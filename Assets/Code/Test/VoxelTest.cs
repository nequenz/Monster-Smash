using UnityEngine;


public class VoxelTest : MonoBehaviour
{
    [SerializeReference] private Transform _testObj;
    [SerializeReference] private IVoxelMesh _voxelMesh = new VoxelMesh();
    [SerializeReference] private IVoxelData _voxels = new VoxelData();
    [SerializeReference] private IVoxelCollider _voxelCollider = new VoxelCollider();
    private MeshFilter _filter;
    private Collider _collider;


    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();
        _collider = GetComponentInChildren<Collider>();

        _voxels.Allocate();
        _voxels.SetVoxel(new Vector3Int(0, 0, 0), 0);
        _voxelMesh.SetVoxelBody(_voxels);
        _voxelMesh.RebuildForced();
        _voxelCollider.SetVoxelMesh(_voxelMesh);
        _voxelCollider.SetVoxelData(_voxels);

        _collider.transform.localScale = _voxels.Size * _voxelMesh.SizeFactor;
        _filter.mesh = _voxelMesh.BuiltMesh;
    }

    private void Update()
    {
        if(transform.position.GetDistance(_testObj.position) < 2)
        {
            _voxelCollider.CalculateVoxelPosition(_testObj.position);
            //Debug.DrawLine(transform.position, _testObj.transform.position, Color.red, 0.1f);
        }
        //_voxelMesh.Update(Time.deltaTime);
        //transform.rotation *= Quaternion.AngleAxis(0.1f,Vector3.up);
    }
}