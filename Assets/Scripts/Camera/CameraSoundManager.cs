using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CameraSoundManager : MonoBehaviour
{
    // Editor variables
    public AudioClip servo1;
    public AudioClip servo2;
    public AudioClip servo3;

    // References
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void TargetAcquired()
    {
        source.time = 0.3f;
        source.PlayOneShot(servo1);
    }

    public void FollowingTarget(float velocity)
    {

    }
}
