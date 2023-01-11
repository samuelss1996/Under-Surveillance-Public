using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMaterialBehaviour : MonoBehaviour
{
    // Editor parameters
    public PhysicsMaterial material;
    public bool hasHoles;

    public void OnElectrified(GameObject electricityArcs)
    {
        if(material.electrical)
        {
            new ElectricArcsWrapper(electricityArcs, gameObject, false);
        }
    }
}
