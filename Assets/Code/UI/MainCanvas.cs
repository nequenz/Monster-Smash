using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    [SerializeReference] CameraScreenView _cameraScreenView = new();


    private void Start()
    {
        _cameraScreenView.CalibrateAimUI();
    }
}
