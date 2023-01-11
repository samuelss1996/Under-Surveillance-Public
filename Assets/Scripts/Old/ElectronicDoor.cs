using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicDoor : MonoBehaviour
{
    // Editor parameters
    public AudioSource openAudio;
    public AudioSource closeAudio;

    // References
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.SetTrigger("Open");
        openAudio.Play();
    }

    public void CloseDoor()
    {
        animator.SetTrigger("Close");
        closeAudio.Play();
    }
}
