using UnityEngine;



public class ProjectileRoot : MonoBehaviour
{
    [SerializeField] private Vector3 _directionToMove;
    private Rigidbody _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();

        _body.AddForce(_directionToMove * 100);
    }
}