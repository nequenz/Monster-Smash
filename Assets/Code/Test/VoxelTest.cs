using UnityEngine;


public class VoxelTest : MonoBehaviour
{
    [SerializeReference] private Transform _testObj;
    [SerializeReference] private IVoxelMesh _voxelMesh = new VoxelMesh();
    [SerializeReference] private IVoxelData _voxels = new VoxelData();
    [SerializeReference] private IVoxelTransform _voxelCollider = new VoxelTransform();
    private MeshFilter _filter;
    private Collider _collider;


    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();
        _collider = GetComponentInChildren<Collider>();

        _voxels.Allocate();
        _voxels.SetVoxel(new Vector3Int(0, 0, 0), 0);
        _voxels.Changed += OnVoxelsChanged;
        _voxelMesh.SetVoxelBody(_voxels);
        _voxelMesh.RebuildForced();
        _voxelCollider.SetVoxelMesh(_voxelMesh);
        _voxelCollider.SetVoxelData(_voxels);

        _collider.transform.localScale = _voxels.Size * _voxelMesh.SizeFactor;
        _filter.mesh = _voxelMesh.BuiltMesh;

        transform.rotation *= Quaternion.AngleAxis(45, Vector3.up);
    }

    private void Update()
    {
        Vector3Int voxelPosition;

        if(transform.position.GetDistance(_testObj.position) < 2)
        {
            voxelPosition = _voxelCollider.CalculateVoxelPosition(_testObj.position);
            _voxels.SetVoxel(voxelPosition, 0);
        }

        //transform.rotation *= Quaternion.AngleAxis(0.1f,Vector3.up);
        //Debug.Log("Y:"+transform.rotation.eulerAngles.y);
    }

    private void OnVoxelsChanged(Vector3Int position)
    {
        //Debug.Log("!");
        _voxelMesh.RebuildForced();
    }
}