using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricArcsWrapper {
    private ElectricityArcsBehaviour[] arcs;

    public ElectricArcsWrapper(GameObject electricityArcs, GameObject target, bool loop)
    {
        Collider[] colliders = target.GetComponents<Collider>();
        arcs = new ElectricityArcsBehaviour[colliders.Length];

        for(int i = 0; i < colliders.Length; i++)
        {
            GameObject arcsGo = Object.Instantiate(electricityArcs, target.transform.position, Quaternion.identity);
            arcs[i] = arcsGo.GetComponent<ElectricityArcsBehaviour>();

            arcs[i].Initiate(colliders[i], loop);
        }
    }

    public void SetActive(bool active)
    {
        foreach(ElectricityArcsBehaviour arc in arcs)
        {
            arc.gameObject.SetActive(active);
        }
    }
}
