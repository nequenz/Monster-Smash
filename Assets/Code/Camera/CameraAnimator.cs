using UnityEngine;
using System.Collections;


public sealed class CameraAnimator
{
    private Transform _transform;
    private WaitForEndOfFrame _seconds = new WaitForEndOfFrame();

    public void AttachTransform(Transform transform) => _transform = transform;

    public IEnumerator ShowEnemy()
    {
        while(true)
        {
            _transform.position += new Vector3(0,0,Time.deltaTime);

            if (_transform.position.z >= 1.0f)
                break;

            yield return null;
        }

        Debug.Log("exit");

        yield return null;
    }
}
