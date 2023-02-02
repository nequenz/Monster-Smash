using UnityEngine;



public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector3 _directionToMove;
    private Rigidbody _body;

    private void Awake()
    {
        const float Speed = 100;

        _body = GetComponent<Rigidbody>();
        _body.AddForce(_directionToMove * Speed);
    }
}