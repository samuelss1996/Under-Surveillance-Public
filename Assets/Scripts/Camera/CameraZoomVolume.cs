using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomVolume : MonoBehaviour
{
    // Editor parameters
    public CameraFollowPlayer camera;

    public Vector3 leftDistance;
    public Vector3 rightDistance;

    // Refs
    private BoxCollider volume;

    private void Awake()
    {
        volume = GetComponent<BoxCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            float t = Mathf.InverseLerp(volume.bounds.min.x, volume.bounds.max.x, other.transform.position.x);
            Vector3 distance = Vector3.Lerp(leftDistance, rightDistance, t);

            camera.distanceToPlayer = distance;
        }
    }
}
