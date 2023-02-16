using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class TestVolume : MonoBehaviour
{
    [SerializeReference] private IVoxelVolume _voxels = new VoxelVolume();
    [SerializeReference] private IVoxelMesh _mesh = new VoxelMesh();
    [SerializeReference] private IVoxelTransform _transform = new VoxelTransform();
    private IVolumeReadOnly<Color> _blueCube;


    public IVoxelVolume AttachedVoxelVolume => _voxels;
    public IVoxelTransform AttachedVoxelTransform => _transform;


    private void Awake()
    {
        ColorVolume colors = new ColorVolume();
        colors.Allocate(new Vector3Int(8, 8, 8), Color.blue);
        colors.SetValue(new Vector3Int(0, 0, 0), Color.red);
        colors.SetValue(new Vector3Int(1, 0, 0), Color.black);
        colors.SetValue(new Vector3Int(2, 7, 0), Color.yellow);
        _blueCube = new VolumeReadOnly<Color>(colors);

        _voxels.SetVolumePrefabToBuild(_blueCube);
        _mesh.SetVoxelVolume(_voxels);
        _mesh.SetColorVolume(_blueCube);
        _transform.SetVolume(_voxels);
        _transform.SetVolumeMesh(_mesh);
        _voxels.Allocate();
        _voxels.MatchToPrefab();
        _mesh.RebuildForced();
    }

    private void OnTriggerStay(Collider other)
    {
        _voxels.SetValue(_transform.CalculateVoxelPosition(other.transform.position), IVoxelVolume.Empty);
    }

    private void Update()
    {
        _mesh.Update(Time.deltaTime);
        transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * 20, Vector3.one);
    }

    public void SetVoxel(Vector3 position, bool typeVoxel)
    {
        _voxels.SetValue(_transform.CalculateVoxelPosition(position), typeVoxel);
    }

    public bool GetVoxel(Vector3 position)
    {
        return _voxels.GetValue(_transform.CalculateVoxelPosition(position));
    }


}