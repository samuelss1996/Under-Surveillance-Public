using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderBehaviour : MonoBehaviour
{
    // Editor parameters
    public bool facingLeft;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<ProtagonistController>().direction = facingLeft ? -1 : 1;
            other.GetComponent<ProtagonistLadderClimb>().enabled = true;
            other.GetComponent<ProtagonistMovementController>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ProtagonistController controller = other.GetComponent<ProtagonistController>();

            if(controller.direction * controller.axis < 0)
            {
                other.transform.Translate(controller.direction * 0.3f * Vector3.left, Space.World);
                other.GetComponent<ProtagonistLadderClimb>().enabled = false;
            }
        }
    }
}
