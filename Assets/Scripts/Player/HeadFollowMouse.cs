using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollowMouse : MonoBehaviour
{
    // Editor parameters
    public GameObject headSpine;
    public float smoothTime;
    public float maxAngle;

    // State
    private Vector3 velocity;
    private bool satDown = false;
    private Vector3 smoothLookPos;

    private void OnEnable()
    {
        smoothLookPos = headSpine.transform.forward;
    }

    private void OnMouseDown()
    {
        if (satDown)
        {
            GetComponent<Animator>().SetTrigger("StandUp");
        }
    }

    void LateUpdate()
    {
        Vector3 lookPos = headSpine.transform.forward;
        Vector3? mousePos = MousePos();

        if(mousePos != null)
        {
            lookPos = (Vector3) mousePos - headSpine.transform.position;
        }

        if(Quaternion.Angle(headSpine.transform.rotation, Quaternion.LookRotation(lookPos)) > maxAngle)
        {
            lookPos = headSpine.transform.forward;
        }

        smoothLookPos = Vector3.SmoothDamp(smoothLookPos, lookPos, ref velocity, smoothTime);
        headSpine.transform.rotation = Quaternion.LookRotation(smoothLookPos);
    }

    private Vector3? MousePos()
    {
        if(satDown)
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2));
        }
        else
        {
            return GetComponent<ProtagonistController>().MouseRayCastEnvironment();
        }
    }

    private Quaternion ClampAngle(Quaternion target, Quaternion pivot, float maxDegrees)
    {
        while(Quaternion.Angle(target, pivot) > maxDegrees)
        {
            target = Quaternion.RotateTowards(target, pivot, 5);
        }

        return target;
    }

    public bool IsSatDown()
    {
        return satDown;
    }

    public void SetSatDown(bool newSatDown)
    {
        satDown = newSatDown;
    }
}
