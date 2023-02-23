using System;
using UnityEngine;

[Serializable]
public class PlayerMove
{
    public const float NoFallingVelocity = 0.1f;

    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _jumpHeight = 2.0f;
    private Rigidbody _body;
    private Transform _transform;
    private Vector3 _prevVelocity;
    private bool _isFalling = false;
    private bool _isMoving = false;
    private bool _canMove = true;
    private bool _canJump = true;


    public Rigidbody AttachedRigidbody => _body;
    public Transform AttachedTransform => _transform;
    public bool IsFalling => _isFalling;
    public bool IsMoving => _isMoving;
    public bool CanMove => _canMove;
    public bool CanJump => _canJump;
    public float Speed => _speed;
    public float JumpHeight => _jumpHeight;


    public void Init(Rigidbody rigidbody, Transform transform)
    {
        SetRigidBody(rigidbody);
        SetTransform(transform);
    }
        
    public void SetRigidBody(Rigidbody rigidbody) => _body = rigidbody;

    public void SetTransform(Transform transform) => _transform = transform;

    public void SetSpeed(float speed) => _speed = speed;

    public void SetJumpHeight(float height) => _jumpHeight = height;

    public void SetMoveMode(bool isEnabled) => _canMove = isEnabled;

    public void SetJumpMode(bool isEnabled) => _canJump = isEnabled;

    public void Move(Vector3 direction)
    {
        _body.AddForce(direction * _speed);
    }

    public void Jump()
    {
        if (_isFalling == false)
            _body.AddForce(Vector3.up * _jumpHeight);
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if (Mathf.Abs(_body.velocity.y - _prevVelocity.y) < NoFallingVelocity)
            _isFalling = false;
        else 
            _isFalling = true;

        _prevVelocity = _body.velocity;
    }

}
