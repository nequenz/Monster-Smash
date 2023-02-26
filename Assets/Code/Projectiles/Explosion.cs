using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _exposionVFX;


    private void Awake()
    {
        _exposionVFX.Stop();
    }

    private void Update()
    {
        if(_exposionVFX.IsAlive() == false)
        {
            Destroy(gameObject);
        }
    }
}