using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    // Editor parameters
    public GameObject target;
    public Vector3 offset;

    public Vector3 limits;
    public PlayerDetectionArea area;

    public float maxSpeed;

    // State
    private Vector3 relaxed;
    private bool previouslyInRange = false;
    private float currentLerp = 1;
    private Vector3 startRotation;

    private void Start()
    {
        relaxed = transform.localEulerAngles;

        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 initialEulers = transform.localEulerAngles;
        transform.LookAt(target.transform);
        Vector3 targetEulers = transform.localEulerAngles;
        transform.localEulerAngles = initialEulers;

        targetEulers += offset;

        NormalizeEuler(ref targetEulers);
        ControlAnimation(targetEulers);

        if (!InRange(targetEulers))
        {
            targetEulers = relaxed;
        }

        transform.localRotation = Quaternion.Slerp(Quaternion.Euler(startRotation), Quaternion.Euler(targetEulers), currentLerp);
        currentLerp += Time.deltaTime;
    }

    private void NormalizeEuler(ref Vector3 eulers)
    {
        for(int i = 0; i < 3; i++)
        {
            eulers[i] = eulers[i] > 180 ? eulers[i] - 360 : eulers[i];
        }
    }

    private bool InRange(Vector3 eulers)
    {
        if(area != null)
        {
            return area.isPlayerInside;
        }

        return eulers.x >= -limits.x && eulers.x <= limits.x
            && eulers.y >= -limits.y && eulers.y <= limits.y
            && eulers.z >= -limits.z && eulers.z <= limits.z;
    }

    private void ControlAnimation(Vector3 targetEulers)
    {
        bool currentlyInRange = InRange(targetEulers);

        if(currentlyInRange != previouslyInRange)
        {
            currentLerp = -0.25f;
            startRotation = transform.localEulerAngles;
        }

        if(currentlyInRange)
        {
            startRotation = transform.localEulerAngles;
        }

        CameraSoundManager sound = GetComponent<CameraSoundManager>();

        if (sound != null)
        {
            if (!previouslyInRange && currentlyInRange)
            {
                sound.TargetAcquired();
            }

            if(currentlyInRange)
            {
                sound.FollowingTarget(Vector3.Distance(startRotation, targetEulers));
            }
        }

        previouslyInRange = currentlyInRange;
    }
}
