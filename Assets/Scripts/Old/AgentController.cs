using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class AgentController : MonoBehaviour
{
    private NavMeshAgent agent;
    private ThirdPersonCharacter character;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveOnClick();

        if(agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, ShouldCrouch(), false);
        } else
        {
            character.Move(Vector3.zero, ShouldCrouch(), false);
        }
    }

    public void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }

    private bool ShouldCrouch()
    {
        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(agent.transform.position, out navMeshHit, 0.1f, NavMesh.AllAreas))
        {
            return navMeshHit.mask == (navMeshHit.mask | (1 << NavMesh.GetAreaFromName("Crouch")));
        }

        return false;
    }

    private void MoveOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
