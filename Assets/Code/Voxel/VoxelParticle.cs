using UnityEngine;
using System;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class VoxelParticle : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshRenderer _render;


    private void Awake()
    {
           
    }

    public void SetSize(float size, Color color)
    {
        transform.localScale *= size;
        _render.material.SetColor("_Color", color);
    }
}