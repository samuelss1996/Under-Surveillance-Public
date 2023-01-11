using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLadder : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Animator anim = other.GetComponent<Animator>();

        if (other.CompareTag("Player") && anim.GetBool("OnLadder")) {

            other.GetComponent<ProtagonistLadderClimb>().climb = false;
            anim.SetTrigger("FinishClimbLadder");
            anim.SetBool("OnLadder", false);
        }
    }
}
