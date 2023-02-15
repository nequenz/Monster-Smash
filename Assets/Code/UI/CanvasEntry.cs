using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEntry : MonoBehaviour
{
    [SerializeReference] AimView _aim = new();
    private RectTransform _mainCanvasRect;
    

    private void Awake()
    {
        _mainCanvasRect = GetComponent<RectTransform>();
        _aim.Init();
    }
}
