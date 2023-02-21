using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _livingTimeMax = 5.0f;
    private Rigidbody _body;
    private Collider _collider;
    private Vector3 _nextPosition;
    private float _livingTime = 0.0f;

    public void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _livingTime = _livingTimeMax;
    }

    private void Update()
    {
        _livingTime -= Time.deltaTime;

        if (_livingTime <= 0.0f)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        _nextPosition = _body.position + _body.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        TestObject body;

        if (collision.collider.Is<TestObject>())
        {
            body = collision.collider.GetComponent<TestObject>();

            if(body.AttachedVoxelBody.GetVoxel(transform.position) == IVoxelVolume.Empty)
            {
                
                //Physics.IgnoreCollision(_collider, collision.collider);
                //Debug.Log("!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TestObject body;

        if (other.Is<TestObject>())
        {
            body = other.GetComponent<TestObject>();

            if (body.AttachedVoxelBody.GetVoxel(transform.position) == IVoxelVolume.Empty)
            {
                //Physics.IgnoreCollision(_collider, collision.collider);
                //Debug.Log("!");
            }
        }
    }

    public void SetForce(Vector3 direction)
    {
        _body.AddForce(direction);
    }
}