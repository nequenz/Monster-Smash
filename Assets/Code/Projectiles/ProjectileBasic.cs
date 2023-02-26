using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class ProjectileBasic : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5.0f;
    [SerializeField] private bool _hasLifeTime = true;
    private Rigidbody _body;
    private WaitForSeconds _waiter;


    public float LifeTime => _lifeTime;
    public bool HasLifeTime => _hasLifeTime;


    private IEnumerator ReduceLifeTime()
    {
        while (true)
        {
            yield return _waiter;

            Destroy(gameObject);
        }
    }

    protected virtual void Awake()
    {
        _body = GetComponent<Rigidbody>();

        if(_hasLifeTime)
        {
            _waiter = new WaitForSeconds(_lifeTime);
            StartCoroutine(ReduceLifeTime());
        }
    }

    protected RaycastHit Raycast(float maxDistance)
    {
        Physics.Raycast(transform.position, _body.velocity, out RaycastHit hit, maxDistance);

        return hit;
    }

    public virtual void SetInitialShotForce(Vector3 direction)
    {
        _body.AddForce(direction);
    }
}