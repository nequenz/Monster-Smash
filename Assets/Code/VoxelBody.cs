using System;
using UnityEngine;


[Serializable]
public class VoxelBody
{
    [SerializeReference] private IVoxelMesh _mesh = new VoxelMesh();
    [SerializeReference] private IVoxelTransform _transform = new VoxelTransform();
    [SerializeReference] private IVoxelVolume _voxels = new VoxelVolume();
    [SerializeField] private VoxelPrefabs _prefabToLoad;


    public IVoxelMesh AttachedVoxelMesh => _mesh;
    public IVoxelTransform AttachedTransform => _transform;
    public IVoxelVolume AttachedVolume => _voxels;


    public void Init()
    {
        _voxels = VoxelPrefab.GetMatchedVoxels(_prefabToLoad);

        _mesh.SetVoxelVolume(_voxels);
        _mesh.SetColorVolume(_voxels.PrefabToBuild);
        _mesh.RebuildForced();

        _transform.SetVolume(_voxels);
        _transform.SetVolumeMesh(_mesh);
    }

    public void Update(float deltaTIme)
    {
        _mesh.Update(deltaTIme);
    }

    public void SetVoxel(Vector3 position, bool typeVoxel)
    {
        _voxels.SetValue(_transform.CalculateVoxelPosition(position), typeVoxel);
    }

    public bool GetVoxel(Vector3 position)
    {
        return _voxels.GetValue(_transform.CalculateVoxelPosition(position));
    }

    public void CalibrateParticle(VoxelParticle particle, Vector3 position)
    {
        particle.SetParams(_mesh.FaceSize, Color.white, position);
    }

}