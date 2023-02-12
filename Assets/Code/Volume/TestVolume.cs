using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class TestVolume : MonoBehaviour
{
    [SerializeReference] private IVoxelVolume _voxels = new VoxelVolume();
    [SerializeReference] private IVoxelMesh _mesh = new VoxelMesh();
    [SerializeReference] private IVoxelTransform _transform = new VoxelTransform();
    [SerializeField] private MeshFilter _meshFilter;
    private IVolumeReadOnly<Color> _blueCube;
    private BoxCollider _collider;


    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();

        ColorVolume colors = new ColorVolume();
        colors.Allocate(new Vector3Int(8, 8, 8), Color.blue);
        colors.SetValue(new Vector3Int(0, 0, 0), Color.red);
        colors.SetValue(new Vector3Int(1, 0, 0), Color.black);
        colors.SetValue(new Vector3Int(2, 7, 0), Color.yellow);
        _blueCube = new VolumeReadOnly<Color>(colors);

        _voxels.SetVolumePrefabToBuild(_blueCube);
        _voxels.Allocate();
        _voxels.Rebuild();
        _mesh.SetColorVolume(_blueCube);
        _mesh.SetVoxelVolume(_voxels);
        _mesh.RebuildForced();
        _transform.SetVolume(_voxels);
        _transform.SetVolumeMesh(_mesh);

        _collider.transform.localScale = _voxels.Size * _mesh.SizeFactor;
        _meshFilter.mesh = _mesh.BuiltMesh;
    }

    
}