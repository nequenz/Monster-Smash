using UnityEngine;
using System;

public interface IVoxelBodyDestroyPolicy
{
    public event Action Destoryed;


    public float MinimumDestroyPersent { get; }


    public void SetMinimumDestroyPersent(float value);

    public void HandleChanges();
}