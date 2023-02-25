using UnityEngine;
using System;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class BodyPart : MonoBehaviour
{
    [SerializeField] private BodyPart _parent;
    [SerializeField] private bool _isRootMain = false;
    private Rigidbody _rigid;


    public event Action<Collision> Collided;
    public event Action Detached;


    public bool IsRootMain => _isRootMain;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();

        if (_parent is not null)
            _parent.Detached += OnParentDetach;
    }

    private void OnCollisionEnter(Collision otherCollision)
    {
        Collided?.Invoke(otherCollision);

        ///TEST
        if(otherCollision.collider.Is<ProjectileBasic>())
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