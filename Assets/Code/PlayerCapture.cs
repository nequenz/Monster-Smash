using UnityEngine;


public sealed class PlayerCapture : MonoBehaviour
{
    [SerializeField] private ActorLiving _capturedActor;
    [SerializeField] private GameCamera _gameCamera;
    [SerializeReference] private LocalInput _input = new();


    public GameCamera AttachedCamera => _gameCamera;
    public ActorLiving AttachedActor => _capturedActor;


    private void Awake()
    {
        InitInputs();
    }

    private void Start()
    {
        _capturedActor.SetTransformToEquip(_gameCamera.transform);
    }

    private void Update()
    {
        _input.Update();
    }

    private void FixedUpdate()
    {
        float yawValue;

        if (_gameCamera is not null)
        {
            yawValue = _gameCamera.transform.rotation.eulerAngles.y;

            _capturedActor.PhysicalBody.MoveRotation(Quaternion.AngleAxis(yawValue, Vector3.up));
        }
    }

    private void InitInputs()
    {
        _input.AttachAction(-1, () => _capturedActor.Movement.Move(_capturedActor.transform.forward), KeyMode.Hold, KeyCode.W);
        _input.AttachAction(-1, () => _capturedActor.Movement.Move(_capturedActor.transform.forward * -1), KeyMode.Hold, KeyCode.S);
        _input.AttachAction(-1, () => _capturedActor.Movement.Move(_capturedActor.transform.right), KeyMode.Hold, KeyCode.D);
        _input.AttachAction(-1, () => _capturedActor.Movement.Move(_capturedActor.transform.right * -1), KeyMode.Hold, KeyCode.A);
        _input.AttachAction(-1, _capturedActor.Movement.Jump, KeyMode.Hold, KeyCode.Space);
        _input.AttachAction(-1, _capturedActor.UseItem, KeyMode.Hold, KeyCode.Mouse0);

        _input.AttachAction(-1, () =>
        {
            if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;

            _gameCamera.SetScreenPosition(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        }, KeyMode.Down, KeyCode.F);

     
    }
}
