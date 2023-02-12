using System.Collections;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private Transform _targetToLook;
    private CameraAnimator _animator = new CameraAnimator();
    private Coroutine _action;


    private void Awake()
    {
        _animator.AttachTransform(transform);

        _action = StartCoroutine(_animator.ShowEnemy());
    }
}
