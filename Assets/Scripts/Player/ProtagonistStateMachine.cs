using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistStateMachine : StateMachineBehaviour
{
    // State
    private bool finished = false;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!finished)
        {
            if (stateInfo.IsName("Idle"))
            {
                finished = true;

                animator.GetComponent<AnimationRotatorCompensator>().OnIdle();
                animator.GetComponent<ProtagonistController>().OnInitialAnimationFinished();
                animator.GetComponent<HeadFollowMouse>().enabled = true;
            }
        }

        if(stateInfo.IsName("SittingIdle"))
        {
            animator.GetComponent<ProtagonistMovementController>().enabled = false;
            animator.GetComponent<HeadFollowMouse>().enabled = false;
        }
        else if(stateInfo.IsName("Stand"))
        {
            animator.GetComponent<ProtagonistLadderClimb>().RepositionOnTop();
        }
        else if(stateInfo.IsName("Walking"))
        {
            animator.GetComponent<ProtagonistMovementController>().enabled = true;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.IsName("WaterAttackLoop") || stateInfo.IsName("WaterAttackLoop 0"))
        {
            animator.GetComponent<ProtagonistWaterController>().UpdateShootingWater();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        if (stateInfo.IsName("SittingIdle"))
        {
            animator.GetComponent<HeadFollowMouse>().SetSatDown(false);
            animator.GetComponent<HeadFollowMouse>().enabled = false;
        }
        else if(stateInfo.IsName("Walking"))
        {
            animator.GetComponent<ProtagonistMovementController>().enabled = false;
        }
    }   
}