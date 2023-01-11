using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDoor : MonoBehaviour
{
    // Editor parameters
    public GameObject forceField;
    public AudioSource fieldSound;
    public AudioSource breakSound;

    public void TurnOff()
    {
        forceField.SetActive(false);

        breakSound.Play();
        fieldSound.Stop();
    }
}
