using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPoolHoleTerminalReact : ElectricTerminalReact
{
    // Editor parameters
    public GameObject drain;
    public WaterPoolBehaviour pool;

    // Refs
    private Animator drainAnim;
    private float timer;

    private void Start()
    {
        drainAnim = drain.GetComponent<Animator>();
    }

    public override void OnInteracted()
    {
        drainAnim.ResetTrigger("Close");
        drainAnim.SetTrigger("Open");
        timer = 1.0f;

        pool.DrainWater();
    }

    public override void Update()
    {
        base.Update();

        if (timer <= 0 && pool.currentHeight <= 0)
        {
            drainAnim.SetTrigger("Close");
        }

        timer -= Time.deltaTime;
    }
}
