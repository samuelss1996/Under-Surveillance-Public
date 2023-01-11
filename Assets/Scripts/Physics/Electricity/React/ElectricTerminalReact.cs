using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElectricTerminalReact : AElectricityReact
{
    // Editor parameters
    public bool powered;
    public bool turnedOn;

    // References
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public virtual void Update()
    {
        if (powered && turnedOn)
        {
            rend.material.EnableKeyword("_EMISSION");
        }
        else
        {
            rend.material.DisableKeyword("_EMISSION");
        }
    }

    public override void OnElectricImpact()
    {
        if(powered && turnedOn)
        {
            OnInteracted();
        }
    }

    public override bool IsInteractable()
    {
        return powered && turnedOn;
    }

    public abstract void OnInteracted();
}
