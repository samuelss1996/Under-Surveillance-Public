using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // Editor 
    public Vector3 distanceToPlayer;
    public float horizontalOvertake;
    public float smoothTime;

    // State
    private Vector3 velocity;

    private void LateUpdate()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 target = player.position + distanceToPlayer;

        target.x += horizontalOvertake * player.forward.x;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }
}
