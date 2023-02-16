using UnityEngine;



public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToLive = 7.0f;
    private Rigidbody _body;
    private float _currentTimeToLive = 0.0f;


    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _currentTimeToLive = _timeToLive;
    }

    private void Update()
    {
        if (_currentTimeToLive > 0.0f)
            _currentTimeToLive -= Time.deltaTime;

        if (_currentTimeToLive <= 0.0f)
            Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.Is<TestVolume>())
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed) => _speed = speed;

    public void AddForce(Vector3 direction) => _body.AddForce(direction.normalized * _speed);
}