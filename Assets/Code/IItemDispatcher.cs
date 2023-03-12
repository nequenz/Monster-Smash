

public interface IItemDispatcher
{
    public void Invoke();

    public void DispatchItemOwner(ActorLiving actorLiving);

    public void DispatchWeaponMechanics(WeaponMechanics weaponMechanics);

    public void Dispatch(ItemM4 itemM4);

    public void Dispatch(ItemRGD itemRGD);
}