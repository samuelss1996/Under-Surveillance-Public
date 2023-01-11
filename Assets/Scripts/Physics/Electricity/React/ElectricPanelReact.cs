using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPanelReact : AElectricityReact
{
    // Editor parameters
    public GameObject electricityArcs;
    public GameObject panel;

    public GameObject[] cables;

    public WaterPoolBehaviour[] connnectingPools;
    public float minPoolHeight;

    public ElectricTerminalReact[] terminals;

    [ColorUsage(true, true)]
    public Color onColor;

    // State
    private ElectricArcsWrapper[] electricWrapper;
    private bool turnedOn;

    private void Awake()
    {
        electricWrapper = new ElectricArcsWrapper[cables.Length];

        for(int i = 0; i < electricWrapper.Length; i++)
        {
            electricWrapper[i] = new ElectricArcsWrapper(electricityArcs, cables[i], true);
            electricWrapper[i].SetActive(false);
        }
    }

    public override void OnElectricImpact()
    {
        panel.GetComponent<Renderer>().material.SetColor("_EmissionColor", onColor);
        turnedOn = true;
    }

    public override bool IsInteractable()
    {
        return !turnedOn;
    }

    private void Update()
    {
        int onCablesCount;
        for (onCablesCount = 0; onCablesCount < connnectingPools.Length && connnectingPools[onCablesCount].GetCurrentHeight() >= minPoolHeight; onCablesCount++);

        onCablesCount++;
        for(int i = 0; i < electricWrapper.Length; i++)
        {
            electricWrapper[i].SetActive(turnedOn && i < onCablesCount);
        }

        UpdateTerminals(turnedOn && onCablesCount == cables.Length);
    }

    private void UpdateTerminals(bool state)
    {
        foreach (ElectricTerminalReact terminal in terminals)
        {
            terminal.powered = state;
        }
    }
}
