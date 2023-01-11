using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioReact : AElectricityReact
{
    // Editor parameters
    public AudioSource music;
    public AudioSource noise;

    public AnimationCurve curve;

    public override void OnElectricImpact()
    {
        StartCoroutine(DistortCR());
    }

    private IEnumerator DistortCR()
    {
        float elapsed = 0;
        float length = curve.keys[curve.length - 1].time;

        noise.Play();

        while (elapsed <= length)
        {
            music.volume = curve.Evaluate(elapsed);

            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
