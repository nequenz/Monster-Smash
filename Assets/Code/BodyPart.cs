using UnityEngine;
using System;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class BodyPart : MonoBehaviour
{
    [SerializeReference] private IVoxelBody _voxelBody = new VoxelBody();
    [SerializeReference] private IVoxelBodyDestroyPolicy _voxelPolicy = new VoxelBodyDestroyPolicy();
    [SerializeField] private BodyPart _parent;
    [SerializeField] private bool _isRootMain = false;
    private Rigidbody _rigid;


    public event Action<Collision> Collided;
    public event Action Detached;


    public bool IsRootMain => _isRootMain;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();

        _voxelBody.SetDestroyPolicy(_voxelPolicy);

        if (_parent is not null)
            _parent.Detached += OnParentDetach;
    }

    private void OnEnable()
    {
        _voxelBody.Collided += OnVoxelBodyCollided;
        _voxelBody.Changed += OnVoxelBodyChanged;
        _voxelPolicy.Destoryed += OnVoxelBodyDestroyed;
    }

    private void OnDisable()
    {
        _voxelBody.Collided -= OnVoxelBodyCollided;
        _voxelBody.Changed-= OnVoxelBodyChanged;
        _voxelPolicy.Destoryed -= OnVoxelBodyDestroyed;
    }

    private void OnCollisionEnter(Collision otherCollision)
    {
        Collided?.Invoke(otherCollision);

        ///TEST
        if(otherCollision.collider.Is<Projectile>())
            Detach();
    }

    private void OnVoxelBodyCollided(Vector3 position)
    {

    }

    private void OnVoxelBodyChanged(Vector3 position)
    {

    }

    private void OnVoxelBodyDestroyed()
    {
        Detach();
    }

    private void OnParentDetach()
    {
        if (_parent.IsRootMain)
            Detach();
        else
            transform.parent = _parent.transform;
    }

    public void Detach()
    {
        _rigid.isKinematic = false;
        transform.parent = null;
        Detached?.Invoke();
    }
}