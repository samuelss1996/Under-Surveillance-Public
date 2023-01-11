using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCanonTrigger : MonoBehaviour
{
    // Editor parameters
    public PhysicsCanon canon;
    public float coolDownTime;

    // State
    private float lastShotAgo;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3? clickPosition = MouseRayCast();

            if(clickPosition != null && lastShotAgo >= coolDownTime)
            {
                canon.Shoot((Vector3) clickPosition);

                lastShotAgo = 0;
            }
        }

        lastShotAgo += Time.deltaTime;
    }

    public Vector3? MouseRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50f) && !hit.transform.gameObject.CompareTag("Player"))
        {
            return hit.point;
        }

        return null;
    }
}
