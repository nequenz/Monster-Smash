using System.Collections;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private Transform _targetToLook;
    private CameraAnimator _animator = new CameraAnimator();
    private Coroutine _action;



}
