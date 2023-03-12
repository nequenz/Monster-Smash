using System;
using UnityEngine;


[Serializable]
public class VoxelBody
{
    [SerializeReference] private IVoxelMesh _mesh = new VoxelMesh();
    [SerializeReference] private IVoxelTransform _transform = new VoxelTransform();
    [SerializeReference] private IVoxelVolume _voxels = new VoxelVolume();
    [SerializeField] private VoxelPrefabs _prefabToLoad;


    public IVoxelMesh AttchedMesh => _mesh;
    public IVoxelTransform AttachedTransform => _transform;
    public IVoxelVolume AttachedVolume => _voxels;


    public void Init()
    {
        _voxels = VoxelModels.GetMatchedVoxels(_prefabToLoad);

        _mesh.SetVoxelVolume(_voxels);
        _mesh.SetColorVolume(_voxels.ModelToBuild);
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

    public void SetVoxels(Vector3 position, float radisu, bool typeVoxel)
    {
        _mesh.SuspendRebuilding();

        _mesh.ResumeRebuilding();
        _mesh.RebuildForced();
    }

    public bool GetVoxel(Vector3 position)
    {
        return _voxels.GetValue(_transform.CalculateVoxelPosition(position));
    }

    public void CalibrateParticle(VoxelParticle particle, Vector3 explosionPosition)
    {
        particle.SetParams(_mesh.FaceSize, Color.white, explosionPosition);
    }

}