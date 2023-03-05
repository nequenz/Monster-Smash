using UnityEngine;


[RequireComponent(typeof(Camera))]
public class GameCamera : MonoBehaviour
{
    [Header("Aim params")]
    [SerializeField] private Vector3 _screenPosition;
    [SerializeField] private float _aimDistance = 50f;
    [SerializeField] private bool _isAutoScreenCenter = true;
    [Header("First person parameters")]
    [SerializeField] private Vector2 _axiesFactor = new Vector2(2.35f, 1.15f);
    [SerializeField] private Vector3 _offset = default;
    [SerializeField] private Transform _attachedBody;
    private Camera _unityCamera;
    private Vector2 _angles;


    public Camera AttachedUnityCamera => _unityCamera;
    public Transform AttachedBody => _attachedBody;
    public Vector3 ScreenPosition => _screenPosition;


    private void Awake()
    {
        _unityCamera = GetComponent<Camera>();

        if (_isAutoScreenCenter)
            _screenPosition = new Vector3(Screen.width / 2, Screen.height / 2);

        if (_attachedBody is not null)
        {
            transform.parent = _attachedBody;
            transform.localPosition = _offset;
        }
    }

    private void Update()
    {
        UpdateFirstPersonView();
    }

    public void SetScreenPosition(Vector3 position) => _screenPosition = position;

    public Vector3 CalculatePositionByDistance(float distance) => Raycast().GetPoint(distance);

    public Ray Raycast() => _unityCamera.ScreenPointToRay(_screenPosition);

    public Vector3 GetAimPosition() => CalculatePositionByDistance(_aimDistance);

    public void UpdateFirstPersonView()
    {
        const float axisXLimit = 80f;

        _angles.y += Input.GetAxis("Mouse X") * _axiesFactor.x;
        _angles.x -= Input.GetAxis("Mouse Y") * _axiesFactor.y;
        _angles.x = Mathf.Clamp(_angles.x, -axisXLimit, axisXLimit);

        transform.rotation = Quaternion.Euler(_angles);
    }
}
