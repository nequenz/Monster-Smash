using UnityEngine;


public enum ItemIDs
{
    Undefined
}

[RequireComponent(typeof(Collider))]
public abstract class Item : MonoBehaviour, IDispatchableItem
{
    [SerializeField] private Vector3 _firstPersonOffset;
    [SerializeField] private Vector3 _firstPersonRotation;
    [SerializeField] private bool _isBlockedToUse = false;
    [SerializeField] private bool _canBeEquipped = true;
    [SerializeField] private EachFrameTimer _usingDelayTimer = new();
    private ActorLiving _owner;


    public ActorLiving Owner => _owner;
    public Transform TransformToEquip => transform.parent;
    public Vector3 FirstPersonOffset => _firstPersonOffset;
    public Vector3 FirstPersonRotation => _firstPersonOffset;
    public bool BlockedToUse => _isBlockedToUse;
    public bool CanBeEquipped => _canBeEquipped;
    public bool IsUsing => _usingDelayTimer.IsRunning;


    private void Awake()
    {
        _usingDelayTimer.Set(OnWhileUsing, null);
    }

    private void Update()
    {
        _usingDelayTimer.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        const float RotationSpeed = 35f;

        if (IsReadyToUse() == false)
        {
            transform.rotation *= Quaternion.AngleAxis(RotationSpeed * Time.deltaTime, Vector3.up);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Is(out ActorLiving actor) && CanBeEquipped)
        {
            actor.EquipItem(this);
        }
    }

    protected void StartUsingTimer()
    {
        _usingDelayTimer.Start();
    }

    protected abstract void OnWhileUsing(float currentDelay, float maxDelay);

    public bool IsReadyToUse()
    {
        return  _owner is not null 
            && transform.parent is not null
            && IsUsing == false
            && _isBlockedToUse == false;
    }

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
        _owner = owner;
    }

    public void SetTransformToEquip(Transform otherTransform)
    {
        transform.parent = otherTransform;
        transform.localPosition = _firstPersonOffset;
        transform.localRotation = Quaternion.Euler(_firstPersonRotation.x, _firstPersonRotation.y, _firstPersonRotation.z);
    }

    public abstract void Use();

    public abstract void AcceptDispatcher(IItemDispatcher itemDispatcher);
}