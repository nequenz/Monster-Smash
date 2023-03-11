using UnityEngine;
using System;

[Serializable]
public class ItemDispatcher : IItemDispatcher
{
    [SerializeField] private PlayerCapture _playerCapture;
    private ActorLiving _itemOwner;
    private ItemM4 _itemM4; 
    private Vector3 _directionToShoot;


    public void SetDirectionToShoot(Vector3 direction)
    {
        _directionToShoot = direction;
    }

    public void Invoke()
    {

    }

    public void DispatchItemOwner(ActorLiving actor)
    {
        _itemOwner = actor;
    }

    public void Dispatch(ItemM4 itemM4)
    {
        _itemM4 = itemM4;
    }

    public void Dispatch(ItemTelepoter itemTelepoter)
    {
     
    }
}