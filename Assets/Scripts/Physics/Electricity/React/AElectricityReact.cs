using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AElectricityReact : MonoBehaviour {
    public abstract void OnElectricImpact();

    public virtual bool IsInteractable()
    {
        return false;
    }
}
