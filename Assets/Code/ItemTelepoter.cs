using UnityEngine;


public sealed class ItemTelepoter : Item
{

    protected override void OnWhileUsing(float maxDelay, float currentDelay)
    {

    }

    public override void Use()
    {
        if(IsReadyToUse())
        {
            Owner.transform.position = Vector3.zero;

            StartUsingTimer();
        }
    }

    public override void AcceptDispatcher(IItemDispatcher itemDispatcher)
    {
        //itemDispatcher.Dispatch(this);
    }
}