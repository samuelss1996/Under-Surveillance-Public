using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Power { Electricity, Water }

public class PowerProvider : MonoBehaviour
{
    // Editor parameters
    public Power power;
    public AudioSource onCollected;
    public ParticleSystem[] particles;

    // State
    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !collected)
        {
            ProtagonistController controller = other.GetComponent<ProtagonistController>();

            switch(power)
            {
                case Power.Electricity:
                    controller.AddElectricPower();
                    break;
                case Power.Water:
                    controller.AddWaterPower();
                    break;
            }

            collected = true;
            onCollected.Play();
            
            foreach(ParticleSystem particle in particles)
            {
                particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }

            controller.NextPower();
        }
    }
}
