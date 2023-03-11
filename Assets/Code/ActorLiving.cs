using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ActorLiving : MonoBehaviour, IDispatchableItem
{   
    [SerializeReference, SubclassSelector] private ActorDynamicMove _move = new();
    [SerializeReference, SubclassSelector] private ItemHolder _itemHolder = new();
    private Rigidbody _body;


    public ActorDynamicMove Movement => _move;
    public Rigidbody PhysicalBody => _body;


    private void Awake()
    {
        _body = GetComponent<Rigidbody>();

        _move.Init(_body, transform);
        _itemHolder.Init(this, transform);
    }

    private void FixedUpdate()
    {
        _move.FixedUpdate(Time.fixedDeltaTime);
    }

    public void UseItem()
    {
        _itemHolder.UseItem();
    }

    public void EquipItem(Item item)
    {
        _itemHolder.EquipItem(item);
    }

    public void SetTransformToEquip(Transform transform)
    {
        _itemHolder.SetTransformToEquip(transform);
    }

    public void AcceptDispatcher(IItemDispatcher itemDispatcher)
    {
        itemDispatcher.DispatchItemOwner(this);
    }
}