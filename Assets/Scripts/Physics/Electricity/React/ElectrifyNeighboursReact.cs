using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrifyNeighboursReact : AElectricityReact
{
    // Editor parameters
    public GameObject electricityArcs;
    public GameObject[] neighbours;

    public override void OnElectricImpact()
    {
        foreach(GameObject neighbour in neighbours)
        {
            new ElectricArcsWrapper(electricityArcs, neighbour, false);
        }
    }
}
