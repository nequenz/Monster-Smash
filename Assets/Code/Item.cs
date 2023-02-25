using UnityEngine;
using System;



public class Item : MonoBehaviour
{
    [Header("Item params")]
    [SerializeField] private Vector3 _firstPersonOffset;
    [SerializeField] private float _usingDelay = 1.0f;
    [SerializeField] private bool _blockedToUse = false;
    [SerializeField] private bool _canBeEquipped = true;
    private EachFrameTimer _usingDelayTimer = new();


    public event Action<Transform> Equipped;
    public event Action Used;


    public bool BlockedToUse => _blockedToUse;
    public bool IsUsing => _usingDelayTimer.IsRunning == false;
    public bool IsEquipped => transform.parent is not null;


    private void Awake()
    {
        _usingDelayTimer.Set(_usingDelay, OnWhileUsing, OnFinishUsing);
        OnAwake();
    }

    private void Update()
    {
        _usingDelayTimer.Update(Time.deltaTime);
        OnUpdate();
    }

    protected virtual void OnAwake() { }

    protected virtual void OnUpdate() { }

    protected virtual void OnUse() 
    {
        Debug.Log("OnUse!");
    }

    protected virtual void OnEquip(Transform parent) 
    {
        Debug.Log("OnEquip!");
    }

    protected virtual void OnWhileUsing(float currentMS, float maxDealy) 
    {
        Debug.Log("OnWhileUsing!");
    }

    protected virtual void OnFinishUsing() 
    {
        Debug.Log("OnFinishUsing!");
    }

    protected void Use()
    {
        if(BlockedToUse == false && IsUsing)
        {
            OnUse();
            Used?.Invoke();
        }
    }

    public void EquipToObject(Transform parent)
    {
        if (parent is null && _canBeEquipped == false)
            return;

        transform.parent = parent;
        transform.localPosition = _firstPersonOffset;

        Equipped?.Invoke(parent);
    }
}