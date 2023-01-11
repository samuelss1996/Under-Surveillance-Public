using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Water2DTool;

public class WaterProjectileBehaviour : MonoBehaviour
{
    // Editor parameters
    public float rippleRadius;
    public float rippleStrength;

    // State
    [HideInInspector]
    public WaterPoolBehaviour currentPool;

    public void OnImpact(RaycastHit hit)
    {
        hit.transform.GetComponent<Water2D_Ripple>()?.AddRippleAtPosition(hit.point, rippleRadius, rippleStrength);

        if(currentPool != null)
        {
            currentPool.AddWater();
        }
    }
}
