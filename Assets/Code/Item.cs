using UnityEngine;


public abstract class Item : MonoBehaviour
{
    [Header("Item params")]
    [SerializeField] private Vector3 _firstPersonOffset;
    [SerializeField] private Vector3 _firstPersonRotation;
    [SerializeField] private float _usingDelay = 1.0f;
    [SerializeField] private bool _blockedToUse = false;
    [SerializeField] private bool _canBeEquipped = true;
    [SerializeField] private bool _isAnimated = true;
    private EachFrameTimer _usingDelayTimer = new();
    private Transform _user;


    public Transform User => _user;
    public bool BlockedToUse => _blockedToUse;
    public bool IsUsing => _usingDelayTimer.IsRunning;
    public bool IsEquipped => transform.parent is not null && _user is not null;
    public bool CanBeEquipped => _canBeEquipped;
    public Vector3 FirstPersonOffset => _firstPersonOffset;

    //tempory
    private void OnTriggerEnter(Collider other)
    {
        if(other.Is(out Player player))
        {
            player.AttachedPlayerInteraction.EquipItem(this);
        }
    }

    protected virtual void Awake()
    {
        _usingDelayTimer.Set(_usingDelay, OnWhileUsing, OnFinishUsing);
    }

    protected virtual void Update()
    {
        _usingDelayTimer.Update(Time.deltaTime);

        const float RotationSpeed = 35f;

        if (_isAnimated && IsEquipped == false)
            transform.rotation *= Quaternion.AngleAxis(RotationSpeed * Time.deltaTime, Vector3.up);
    }

    protected abstract void OnUse();

    protected abstract void OnEquip(Transform parent);

    protected abstract void OnUnequip();

    protected abstract void OnWhileUsing(float currentMS, float maxDelay);

    protected abstract void OnFinishUsing();

    public void SetBlockMode(bool isBlocked)
    {
        _blockedToUse = isBlocked;
    }

    public void SetEquipMode(bool canBeEquipped)
    {
        _canBeEquipped = canBeEquipped;
    }

    public void Use()
    {
        if(BlockedToUse == false && IsUsing == false && IsEquipped)
        {
            OnUse();
            _usingDelayTimer.Start();
        }
    }

    public void EquipToObject(Transform parent, Transform user)
    {
        if (parent is null || user is null && _canBeEquipped == false)
            return;

        Unequip();

        _user = user;
        transform.parent = parent;
        transform.localRotation = Quaternion.Euler(_firstPersonRotation.x, _firstPersonRotation.y, _firstPersonRotation.z);
        transform.localPosition = _firstPersonOffset;

        OnEquip(parent);
    }

    public void Unequip()
    {
        if(IsEquipped)
        {
            transform.parent = null;
            _user = null;

            OnUnequip();
        }
            
    }
}