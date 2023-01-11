using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricImpact : MonoBehaviour
{
    // Editor parameters
    public GameObject electricityArcs;

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<AElectricityReact>()?.OnElectricImpact();
        collision.gameObject.GetComponent<PhysicsMaterialBehaviour>()?.OnElectrified(electricityArcs);
    }
}
