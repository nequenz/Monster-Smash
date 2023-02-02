using UnityEngine;
using System;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class BodyPart : MonoBehaviour
{
    [SerializeReference] private IVoxelBody _voxelBody;
    [SerializeReference] private IVoxelBodyDestroyPolicy _voxelPolicy;
    [SerializeField] private BodyPart _parent;  


    public event Action<Collider> Collided;


    private void Awake()
    {

        _voxelBody.SetDestroyPolicy(_voxelPolicy);
    }

    private void OnEnable()
    {
        _voxelBody.Collided += VoxelBodyCollided;
        _voxelBody.Changed += VoxelBodyChanged;
        _voxelPolicy.Destoryed += VoxelBodyDestroyed;
    }

    private void OnDisable()
    {
        _voxelBody.Collided -= VoxelBodyCollided;
        _voxelBody.Changed-= VoxelBodyChanged;
        _voxelPolicy.Destoryed -= VoxelBodyDestroyed;
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        Collided?.Invoke(otherCollider);
    }

    private void VoxelBodyCollided(Vector3 position)
    {

    }

    private void VoxelBodyChanged(Vector3 position)
    {

    }

    private void VoxelBodyDestroyed()
    {

    }
}