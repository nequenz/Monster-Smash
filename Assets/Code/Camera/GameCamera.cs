using UnityEngine;


[RequireComponent(typeof(Camera))]
public class GameCamera : MonoBehaviour
{
    [SerializeReference] CameraRaycast _raycast = new CameraRaycast();
    private Camera _camera;


    public Camera AttachedCamera => _camera;


    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        
    }
}
