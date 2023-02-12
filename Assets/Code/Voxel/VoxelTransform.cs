using UnityEngine;
using System;

[Serializable]
public class VoxelTransform : IVoxelTransform
{
    [SerializeReference] private Transform _transform;
    [SerializeReference] private BoxCollider _collider;
    private IVoxelVolume _voxels;
    private IVoxelMesh _mesh;


    public Transform AttachedTransform => _transform;
    public IVoxelVolume AttachedVoxelVolume => _voxels;
    public IVoxelMesh AttachedVoxelMesh => _mesh;
    public BoxCollider AttachedBoxCollider => _collider;


    private void OnMeshRebuild()
    {
        _collider.transform.localScale = _voxels.Size * _mesh.FaceSize;
    }

    public IVoxelTransform Init(Transform transform, IVoxelVolume volume, IVoxelMesh mesh, BoxCollider collider)
    {
        //SetTransform(transform);
        SetVolume(volume);
        SetVolumeMesh(mesh);
        //SetBoxCollider(collider);

        return this;
    }

    public void SetTransform(Transform transform) => _transform = transform;

    public void SetVolume(IVoxelVolume voxels) => _voxels = voxels;

    public void SetVolumeMesh(IVoxelMesh mesh)
    {
        _mesh = mesh;
        mesh.Rebuilt += OnMeshRebuild;
    }

    public void SetBoxCollider(BoxCollider collider) => _collider = collider;

    public Vector3Int CalculateVoxelPosition(Vector3 position)
    {
        Vector3Int scaledLocalPosition = default;
        Vector3 offsetPosition = _transform.position;
        /*
        Vector3Int scaledLocalPosition = default;
        Vector3 offsetPosition = _transform.position
            - (_data.Size / 2) * _mesh.SizeFactor
            + (Vector3.one * _mesh.SizeFactor / 2);

        Vector3 localPosition = (position - offsetPosition);
        */
        Vector3 localPosition = Quaternion.Inverse(_transform.rotation)
            * (position - offsetPosition)
            + (_voxels.Size / 2) * _mesh.FaceSize;
           // - (Vector3.one * _mesh.SizeFactor / 4);

        scaledLocalPosition.x = (int)(localPosition.x / _mesh.FaceSize);
        scaledLocalPosition.y = (int)(localPosition.y / _mesh.FaceSize);
        scaledLocalPosition.z = (int)(localPosition.z / _mesh.FaceSize);

        //Debug.Log(scaledLocalPosition);
        //Debug.DrawLine(offsetPosition, offsetPosition + localPosition, Color.black, 0.1f);
        return scaledLocalPosition;
    }

}