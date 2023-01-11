using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SaveSystem.Save(other.GetComponent<ProtagonistController>());
        }
    }
}
