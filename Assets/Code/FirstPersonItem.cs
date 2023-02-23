using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirstPersonItem : MonoBehaviour
{
    [SerializeField] private GameCamera _camera;
    private Quaternion _savedRotation;


    private void Awake()
    {
        _savedRotation = transform.localRotation;   
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(_camera.GetAimPosition());
        transform.rotation *= _savedRotation;

    }

}
