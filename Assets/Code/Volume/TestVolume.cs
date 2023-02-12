using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class TestVolume : MonoBehaviour
{
    [SerializeReference] private IVoxelVolume _voxels = new VoxelVolume();
    [SerializeReference] private IVoxelMesh _mesh = new VoxelMesh();
    [SerializeReference] private IVoxelTransform _transform = new VoxelTransform();
    private IVolumeReadOnly<Color> _blueCube;


    private void Awake()
    {
        ColorVolume colors = new ColorVolume();
        colors.Allocate(new Vector3Int(8, 8, 8), Color.blue);
        colors.SetValue(new Vector3Int(0, 0, 0), Color.red);
        colors.SetValue(new Vector3Int(1, 0, 0), Color.black);
        colors.SetValue(new Vector3Int(2, 7, 0), Color.yellow);
        _blueCube = new VolumeReadOnly<Color>(colors);

        _voxels.Init(_blueCube, false).Allocate();
        _voxels.Rebuild();
        _mesh.Init(_voxels, _blueCube, null, 0.55f, 0.25f).RebuildForced();
        _transform.Init(null, _voxels, _mesh, null);
    }

    
}