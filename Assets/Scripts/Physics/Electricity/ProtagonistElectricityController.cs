using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistElectricityController : ProtagonistPowerController
{
    // Editor parameters
    public PhysicsCanon electricityCanon;
    public float rotationTime;
    public float throwDelay;
    public float coolDownTime;
    public float earlyClickTime;

    // State
    [HideInInspector]
    public bool cooledDown = true;
    private bool canClick = true;

    void Update()
    {
        if (canClick && !GetComponent<ProtagonistLadderClimb>().enabled)
        {
            StartCoroutine(ThrowElectricityCR());
        }
    }

    private IEnumerator ThrowElectricityCR()
    {
        Vector3? clickPosition = CheckHitPosition();

        if(clickPosition != null)
        {
            canClick = false;

            while(!cooledDown)
            {
                yield return new WaitForEndOfFrame();
            }

            cooledDown = false;

            GetComponent<ProtagonistMovementController>().enabled = false;
            GetComponent<Animator>().SetTrigger("CastSpell");
            float elapsed = 0f;

            Vector3 lookPos = (Vector3)clickPosition - transform.position;
            lookPos.y = 0;
            Quaternion initial = transform.rotation;
            Quaternion target = Quaternion.LookRotation(lookPos);

            while (elapsed <= rotationTime)
            {
                transform.rotation = Quaternion.Slerp(initial, target, elapsed / rotationTime);
                elapsed += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(throwDelay - rotationTime);
            electricityCanon.Shoot((Vector3)clickPosition);

            yield return new WaitForSeconds(coolDownTime - throwDelay - rotationTime - earlyClickTime);
            canClick = true;

            yield return new WaitForSeconds(earlyClickTime);
            cooledDown = true;
        }
    }
}
