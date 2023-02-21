using UnityEngine;
using System;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class VoxelParticle : MonoBehaviour
{
    [SerializeField] float _detachForce = 8.0f;

    private Rigidbody _rigidbody;
    private MeshRenderer _render;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _render = GetComponent<MeshRenderer>();
    }

    public void SetParams(float size, Color color,Vector3 explosionPosition)
    {
        transform.localScale *= size;
        _rigidbody.AddExplosionForce(_detachForce, explosionPosition, _detachForce);
    }
}