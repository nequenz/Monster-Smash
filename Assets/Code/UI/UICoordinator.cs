using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICoordinator : MonoBehaviour
{
    [SerializeField] private RectTransform _aimRect;
    private RectTransform _mainCanvasRect;
    

    private void Awake()
    {
        _mainCanvasRect = GetComponent<RectTransform>();

        _aimRect.position = new Vector3(_mainCanvasRect.rect.width / 2, _mainCanvasRect.rect.height / 2);
    }
}
