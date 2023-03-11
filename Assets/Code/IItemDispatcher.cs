using UnityEngine;

public interface IItemDispatcher
{
    public void SetDirectionToShoot(Vector3 direction);

    public void Invoke();

    public void DispatchItemOwner(ActorLiving actorLiving);

    public void Dispatch(ItemM4 itemM4);

    public void Dispatch(ItemTelepoter itemTelepoter);
}