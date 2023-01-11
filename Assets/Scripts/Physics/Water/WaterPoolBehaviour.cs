using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Water2DTool;

public class WaterPoolBehaviour : MonoBehaviour
{
    // Editor parameters
    public GameObject waterBoxPrefab;
    public float volumePerWaterUnit;
    public float initialHeight;
    public float drainMultiplier;

    // References
    private BoxCollider box;
    private Water2D_Tool water;
    public float currentHeight = 0;

    // State
    private bool pendingUpdate;

    private void Awake()
    {
        currentHeight = initialHeight;
        box = GetComponent<BoxCollider>();

        GameObject waterGo = Instantiate(waterBoxPrefab, Vector3.zero, Quaternion.identity);
        water = waterGo.GetComponent<Water2D_Tool>();

        waterGo.gameObject.tag = gameObject.tag;
        gameObject.tag = "Untagged";

        UpdateWaterMesh();
    }

    public void AddWater()
    {
        float newHeight = currentHeight + volumePerWaterUnit / box.bounds.size.x / box.bounds.size.z;
        currentHeight = Mathf.Clamp(newHeight, 0, box.bounds.size.y);

        UpdateWaterMesh();
    }

    public void DrainWater()
    {
        StartCoroutine(DrainWaterCR());
    }

    private IEnumerator DrainWaterCR()
    {
        while (currentHeight > 0)
        {
            float newHeight = currentHeight - drainMultiplier * Time.deltaTime * volumePerWaterUnit / box.bounds.size.x / box.bounds.size.z;
            currentHeight = Mathf.Clamp(newHeight, 0, box.bounds.size.y);

            UpdateWaterMesh();
            yield return new WaitForEndOfFrame();
        }
    }

    private void UpdateWaterMesh()
    {
        pendingUpdate = true;
    }

    private void FixedUpdate()
    {
        if(pendingUpdate)
        {
            water.UpdateWaterMesh(box.bounds.size.x, currentHeight, box.bounds.size.z, true);
            water.transform.position = CalculateBasePosition();

            pendingUpdate = false;
        }
    }

    private Vector3 CalculateBasePosition()
    {
        Vector3 basePos = box.bounds.center;

        basePos += box.bounds.size.y / 2 * Vector3.down;
        basePos += box.bounds.size.z / 2 * Vector3.back;
        basePos += currentHeight / 2 * Vector3.up;

        return basePos;
    }


    private void OnTriggerEnter(Collider other)
    {
        UpdateProjectilePool(other, true);
    }

    private void OnTriggerExit(Collider other)
    {
        UpdateProjectilePool(other, false);
    }

    private void UpdateProjectilePool(Collider other, bool inPool)
    {
        WaterProjectileBehaviour waterProj = other.GetComponent<WaterProjectileBehaviour>();

        if (waterProj != null)
        {
            waterProj.currentPool = inPool? this : null;
        }
    }

    public float GetCurrentHeight()
    {
        return currentHeight;
    }
}
