using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LevelEvent
{
    public Vector3 playerDestination;
    public float moveDelay;
    public UnityEvent callback;

    [HideInInspector]
    public bool completed;
}

public class LevelEvents : MonoBehaviour
{
    // Editor perameters
    public LevelEvent[] events;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < events.Length; i++)
        {
            if(Input.GetKeyDown((i + 1).ToString()))
            {
                events[i].callback.Invoke();
                StartCoroutine(MovePlayer(i));
            }
        }
    }

    private IEnumerator MovePlayer(int index)
    {
        yield return new WaitForSeconds(events[index].moveDelay);

        int destinationIndex = -1;
        events[index].completed = true;

        for (int i = 0; i < events.Length; i++)
        {
            if (!events[i].completed)
            {
                break;
            }

            destinationIndex++;
        }

        if (destinationIndex >= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<AgentController>()
                .SetDestination(events[destinationIndex].playerDestination);
        }
    }


    /** ### CODE USED ONLY FOR ORIGINAL CHAAPTER 0 HERE ### **/
    /*private void HackDoor()
    {
        initialRoom.GetComponent<Renderer>().material = openRoomMaterial;
        doorHackSound.Play();
        completedEvents[0] = true;

        MovePlayer();
    }

    private void OpenDoor()
    {
        initialRoom.GetComponent<Animator>().SetTrigger("OpenDoor");
        StartCoroutine(DoorTraverseDelay());
    }

    private void OpenBarrier(int index) {
        barriers[index].GetComponent<BarrierDoor>()?.TurnOff();

        completedEvents[index + 2] = true;
        MovePlayer();
    }

    private void RemoveSetOfTrash()
    {
        //electronicDoor.GetComponent<ElectronicDoor>().CloseDoor();
        setOfTrash.SetActive(false);

        completedEvents[6] = true;
    }

    private void OpenElectronicDoor()
    {
        electronicDoor.GetComponent<ElectronicDoor>().OpenDoor();
        completedEvents[7] = true;

        MovePlayer();
        FindObjectOfType<Alarm>().turnedOn = true;
    }

    private IEnumerator DoorTraverseDelay()
    {
        yield return new WaitForSeconds(0.75f);

        doorOpenSound.Play();

        yield return new WaitForSeconds(0.75f);

        completedEvents[1] = true;
        MovePlayer();
    }*/
}
