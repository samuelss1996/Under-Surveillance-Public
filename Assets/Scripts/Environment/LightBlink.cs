using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : AElectricityReact
{
    // Editor parameters
    public Light[] lights;
    public float intensityMultiplier;
    public AudioClip electricSpark;
    public bool initiallyTurnedOn;

    // Refs
    private AudioSourceLoudnessTester loudness;
    private AudioSource source;
    private Material material;

    // State
    private bool started;

    void Awake()
    {
        loudness = GetComponent<AudioSourceLoudnessTester>();
        source = GetComponent<AudioSource>();
        material = GetComponent<Renderer>().material;
        started = initiallyTurnedOn;
    }

    void Update()
    {
        foreach(Light light in lights)
        {
            if(started)
            {
                light.intensity = (0.06f - loudness.clipLoudness) * intensityMultiplier;
            }
            else
            {
                light.intensity = 0;
            }

            material.SetColor("_EmissionColor", Mathf.Lerp(0, 0.95f, 3 * light.intensity) * Color.white);
        }
    }

    public void OnNewGame()
    {
        StartCoroutine(BlinkAfterDelay());
    }

    public IEnumerator BlinkAfterDelay()
    {
        yield return new WaitForSeconds(4f);

        source.Play();
        started = true;

        HeadFollowMouse headFollow = FindObjectOfType<HeadFollowMouse>();
        headFollow.SetSatDown(true);
        headFollow.enabled = true;
    }

    public override void OnElectricImpact()
    {
        started = true;

        source.clip = electricSpark;
        source.Play();
    }
}
