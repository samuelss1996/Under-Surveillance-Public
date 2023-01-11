using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRotatorCompensator : MonoBehaviour
{
    // State
    private Vector3 rotation;

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(rotation) * transform.rotation;
        rotation = Vector3.zero;
    }

    public void OnIdle()
    {
        StartCoroutine(RotateOverTransitionCR(-90, 0.5f));
    }

    public IEnumerator RotateOverTransitionCR(float degrees, float transitionTime)
    {
        float elapsed = 0;

        do
        {
            elapsed += Time.deltaTime;

            float rotValue = degrees / transitionTime * Time.deltaTime;
            rotation = new Vector3(0, rotValue, 0);

            yield return new WaitForEndOfFrame();
        } while (elapsed <= transitionTime);
    }
}
