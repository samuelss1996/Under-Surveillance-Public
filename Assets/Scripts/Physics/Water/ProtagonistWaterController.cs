using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistWaterController : ProtagonistPowerController
{
    // Editor parameters
    public PhysicsCanon waterCanon;
    public float coolDownTime;
    public float smoothRotationTime;

    // References
    private Animator animator;

    // State
    private Vector3? heldPosition;
    private Vector3 rotationVelocity;
    private float lastShotAgo;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!GetComponent<ProtagonistLadderClimb>().enabled)
        {
            if (CheckHitPosition() != null)
            {
                animator.SetTrigger("ThrowWater");
                animator.ResetTrigger("StopWater");
            }
            else if (Input.GetMouseButtonUp(0))
            {
                animator.SetTrigger("StopWater");
                animator.ResetTrigger("ThrowWater");
            }
            else if (Input.GetMouseButton(0))
            {
                FixRotation();
            }
        }

        lastShotAgo += Time.deltaTime;
    }

    private void FixRotation()
    {
        heldPosition = CheckHeldPosition();

        if (heldPosition != null)
        {
            Vector3 targetLookAt = (Vector3)heldPosition - transform.position;
            targetLookAt.y = 0;
            targetLookAt.Normalize();

            Vector3 smoothLookAt = Vector3.SmoothDamp(transform.forward, targetLookAt, ref rotationVelocity, smoothRotationTime);
            transform.rotation = Quaternion.LookRotation(smoothLookAt);
        }
    }

    public void UpdateShootingWater()
    {
        if (heldPosition != null && lastShotAgo >= coolDownTime)
        {
            waterCanon.Shoot((Vector3)heldPosition);
            lastShotAgo = 0;
        }
    }
}
