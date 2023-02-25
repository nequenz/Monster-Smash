using UnityEngine;
using System;



public abstract class Item : MonoBehaviour
{
    [Header("Item params")]
    [SerializeField] private Vector3 _firstPersonOffset;
    [SerializeField] private float _usingDelay = 1.0f;
    [SerializeField] private bool _blockedToUse = false;
    [SerializeField] private bool _canBeEquipped = true;
    [SerializeField] private bool _isAnimated = true;
    private EachFrameTimer _usingDelayTimer = new();


    public bool BlockedToUse => _blockedToUse;
    public bool IsUsing => _usingDelayTimer.IsRunning;
    public bool IsEquipped => transform.parent is not null;
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
        if(BlockedToUse == false && IsUsing == false && transform.parent is not null)
        {
            _usingDelayTimer.Start();
            OnUse();
        }
    }

    public void EquipToObject(Transform parent)
    {
        if (parent is null && _canBeEquipped == false)
            return;

        if (transform.parent is not null)
            Unequip();

        transform.parent = parent;
        transform.localPosition = _firstPersonOffset;

        OnEquip(parent);
    }

    public void Unequip()
    {
        if(transform.parent is not null)
            transform.parent = null;
    }
}