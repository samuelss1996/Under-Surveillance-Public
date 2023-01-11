using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretBehaviour : MonoBehaviour
{
    // Editor parameters
    public AudioSource playBack;
    public AudioSource grab;
    public ParticleSystem[] particles;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            grab.Play();
            playBack.Stop();

            foreach(ParticleSystem particle in particles)
            {
                particle.Stop(true);
            }
        }
    }
}
