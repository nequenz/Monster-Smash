using UnityEngine;


public enum ItemIDs
{
    Undefined
}

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Item : MonoBehaviour, IDispatchableItem
{
    [SerializeField] private Vector3 _firstPersonOffset;
    [SerializeField] private Vector3 _firstPersonRotation;
    [SerializeField] private ItemIDs _id;
    [SerializeField] private bool _isBlockedToUse = false;
    [SerializeField] private bool _canBeEquipped = true;
    [SerializeField] private EachFrameTimer _usingDelayTimer = new();
    protected ActorLiving Owner;
    protected Transform TransformToEquip;


    public ItemIDs ID => _id;
    public Vector3 FirstPersonOffset => _firstPersonOffset;
    public Vector3 FirstPersonRotation => _firstPersonOffset;
    public bool BlockedToUse => _isBlockedToUse;
    public bool CanBeEquipped => _canBeEquipped;


    protected abstract void OnSetTransformToEquip(Transform transform);

    protected abstract void OnSetOwner(ActorLiving owner);

    protected abstract void OnUse();

    public void SetBlockedMode(bool isBlocked)
    {
        _isBlockedToUse = isBlocked;
    }

    public void SetEquipMode(bool canBeEquipped)
    {
        _canBeEquipped = canBeEquipped;
    }

    public void SetOwner(ActorLiving owner)
    {
        Owner = owner;

        OnSetOwner(owner);
    }

    public void SetTransformToEquip(Transform transform)
    {
        TransformToEquip = transform;

        OnSetTransformToEquip(transform);
    }

    public bool TryUse()
    {
        if (_usingDelayTimer.IsRunning == false)
        {
            OnUse();

            return true;
        }

        return false;
    }

    public abstract void AcceptDispatch(IItemDispatcher itemDispatcher);
}