using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _exposionVFX;


    private void Update()
    {
        if(_exposionVFX.IsAlive() == false)
        {

            Destroy(gameObject);
        }
    }
}