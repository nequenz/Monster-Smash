using UnityEngine;
using System;
using System.Collections;


public sealed class CameraAnimator
{
    private Transform _transform;


    public void AttachTransform(Transform transform) => _transform = transform;

    public IEnumerator ShowEnemy()
    {
        while(true)
        {
            
        }

        yield return null;
    }
}
