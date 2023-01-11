using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    // Editor parameters
    public AudioSource open;
    public AudioSource close;

    // State
    private Animator animator;
    private AnimatorStateInfo previousState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        previousState = animator.GetCurrentAnimatorStateInfo(0);
    }

    private void Update()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if(state.fullPathHash != previousState.fullPathHash)
        {
            if(state.IsName("Open"))
            {
                open.Play();
            }
            else if(state.IsName("Close"))
            {
                close.Play();
            }
        }

        previousState = state;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            animator.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            animator.SetTrigger("Close");
        }
    }
}
