using UnityEngine;
using System;


[Serializable]
public class ItemHolder
{
    [SerializeReference, SubclassSelector] private IItemDispatcher _itemDispatcher = new ItemDispatcher();
    private ActorLiving _owner;
    private Transform _transformToEquip;
    private Item _currentItem;


    public void Init(ActorLiving actorLiving, Transform transform)
    {
        _owner = actorLiving;
        _transformToEquip = transform;
    }

    public void EquipItem(Item item)
    {
        if (item is null)
            return;

        _currentItem = item;
        _currentItem.SetOwner(_owner);
        _currentItem.SetTransformToEquip(_transformToEquip);
    }

    public void SetTransformToEquip(Transform transform)
    {
        _transformToEquip = transform;
    }

    public void UseItem()
    {
        if (_currentItem is null || _owner is null)
            return;

        _owner.AcceptDispatcher(_itemDispatcher);
        _currentItem.AcceptDispatcher(_itemDispatcher);
        _itemDispatcher.Invoke();
        _currentItem.Use();
    }
}