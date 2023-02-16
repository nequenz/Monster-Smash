using UnityEngine;


[RequireComponent(typeof(Camera))]
public class GameCamera : MonoBehaviour
{
    [SerializeReference] CameraRaycast _raycast = new();
    [SerializeReference] CameraInput _input = new();
    private Camera _camera;


    public Camera AttachedCamera => _camera;
    public Transform Transform => _camera.transform;


    private void Awake()
    {
        _camera = GetComponent<Camera>();

        _raycast.Init(this);
        _input.Init(this);
    }

    private void Update()
    {
        _input.Update(Time.deltaTime);
    }
}
