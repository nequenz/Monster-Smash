using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ActorLiving : MonoBehaviour
{   
    [SerializeReference, SubclassSelector] private ActorDynamicMove _move = new();
    private ItemHolder _itemHolder = new();
    private Rigidbody _body;


    public ActorDynamicMove Movement => _move;
    public Rigidbody PhysicalBody => _body;


    private void Awake()
    {
        _body = GetComponent<Rigidbody>();

        _move.Init(_body, transform);
    }

    private void FixedUpdate()
    {
        _move.FixedUpdate(Time.fixedDeltaTime);
    }
}