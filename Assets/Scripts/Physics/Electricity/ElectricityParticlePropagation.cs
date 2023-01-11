using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityParticlePropagation : MonoBehaviour
{
    // Editor variables
    public GameObject electricityParticlePrefab;

    private void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < 20; i++)
        {
            Instantiate(electricityParticlePrefab, RandomPointInBounds(other.bounds), transform.rotation);
        }
    }

    private Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
