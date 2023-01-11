using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButtonReact : ElectricTerminalReact
{
    // Editor parameters
    public ElevatorPlatformBehaviour elevator;
    public int steps;

    public override void OnInteracted()
    {
        elevator.MoveElevator(steps);
    }
}
