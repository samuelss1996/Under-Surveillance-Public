using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProtagonistPowerController : MonoBehaviour
{
    // Editor parameters
    public GameObject effect;

    private void OnEnable()
    {
        effect.SetActive(true);
    }

    protected Vector3? CheckHitPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return GetComponent<ProtagonistController>().MouseRayCastEnvironment();
        }

        return null;
    }

    protected Vector3? CheckHeldPosition()
    {
        if(Input.GetMouseButton(0))
        {
            return GetComponent<ProtagonistController>().MouseRayCastEnvironment();
        }

        return null;
    }

    protected Vector3? CheckReleasePosition()
    {
        if (Input.GetMouseButtonUp(0))
        {
            return GetComponent<ProtagonistController>().MouseRayCastEnvironment();
        }

        return null;
    }

    private void OnDisable()
    {
        effect.SetActive(false);
    }
}
