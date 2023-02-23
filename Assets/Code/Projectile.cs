using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _livingTimeMax = 5.0f;
    private Rigidbody _body;
    private Collider _collider;
    private Vector3 _prevVelocity;
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
        if(Physics.Raycast(transform.position, _body.velocity, out RaycastHit hit, 2f))
        {
            if(hit.collider.Is(out TestObject body))
                body.AttachedVoxelBody.SetVoxel(hit.point, IVoxelVolume.Empty);
        }
    }

    public void SetForce(Vector3 direction)
    {
        _body.AddForce(direction);
    }
}