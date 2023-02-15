using UnityEngine;


[RequireComponent(typeof(Camera))]
public class GameCamera : MonoBehaviour
{
    [SerializeReference] CameraRaycast _cameraRayCast = new();
    [SerializeReference] CameraInput _cameraInput = new();
    private Camera _camera;


    public Camera AttachedCamera => _camera;
    public CameraRaycast CameraRaycast => _cameraRayCast;
    public CameraInput Input => _cameraInput;


    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _cameraRayCast.Init();
    }

    private void Update()
    {
        
    }
}
