using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5.0f;
    [SerializeField] private bool _hasLifeTime = true;
    private Rigidbody _body;
    private WaitForSeconds _waiter;


    public float LifeTime => _lifeTime;
    public bool HasLifeTime => _hasLifeTime;


    private void Awake()
    {
        _body = GetComponent<Rigidbody>();

        if(_hasLifeTime)
        {
            _waiter = new WaitForSeconds(_lifeTime);
            StartCoroutine(ReduceLifeTime());
        }
    }

    private void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, _body.velocity, out RaycastHit hit, 2f))
        {
            if(hit.collider.Is(out TestObject body))
                body.AttachedVoxelBody.SetVoxel(hit.point, IVoxelVolume.Empty);
        }
    }

    private IEnumerator ReduceLifeTime()
    {
        while (true)
        {
            yield return _waiter;

            Destroy(gameObject);
        }
    }

    protected abstract void OnAwake();

    protected abstract void OnFixedUpdate();

    protected RaycastHit Raycast(float maxDistance)
    {
        Physics.Raycast(transform.position, _body.velocity, out RaycastHit hit, maxDistance);

        return hit;
    }

    public virtual void SetShotForce(Vector3 direction)
    {
        _body.AddForce(direction);
    }
}