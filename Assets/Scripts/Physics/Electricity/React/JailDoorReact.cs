using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailDoorReact : AElectricityReact
{
    // Editor parameters
    public Animator animator;

    // State
    private bool opened;

    public override void OnElectricImpact()
    {
        if(!opened)
        {
            opened = true;
            StartCoroutine(OpenDoorCR());
        }
    }

    private IEnumerator OpenDoorCR()
    {
        animator.SetTrigger("OpenDoor");

        yield return new WaitForSeconds(1.0f);

        FindObjectOfType<CameraFollowPlayer>().horizontalOvertake = 1;
    }
}
