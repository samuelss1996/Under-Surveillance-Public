using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityArcsBehaviour : MonoBehaviour
{
    // Editor parameters
    public ParticleSystem arcsParticles;
    public ParticleSystem sparksParticles;
    public float rateMultiplier;
    public AnimationCurve electricityCurve;

    // Values
    private float maxLightIntensity;
    private float maxLightRange;

    // State
    private float currentAnimTime = -1;
    private float currentBoxVolume;

    // Other
    private Collider hitCollider;
    private bool loop;

    private void Awake()
    {
        maxLightIntensity = sparksParticles.lights.intensityMultiplier;
        maxLightRange = sparksParticles.lights.rangeMultiplier;
    }

    private void Start()
    {
        OnObjectHit();
    }

    public void Initiate(Collider collider, bool looping)
    {
        hitCollider = collider;
        loop = looping;
    }

    void Update()
    {
        if (loop)
        {
            ChangeIntensity(1);
        }
        else if (currentAnimTime >= 0  && currentAnimTime <= electricityCurve[electricityCurve.length - 1].time)
        {
            float intensity = electricityCurve.Evaluate(currentAnimTime);
            ChangeIntensity(intensity);

            currentAnimTime += Time.deltaTime;
        }
        else if(currentAnimTime > electricityCurve[electricityCurve.length - 1].time)
        {
            arcsParticles.Stop();
            sparksParticles.Stop();

            Destroy(gameObject);
        }
    }

    private void OnObjectHit()
    {
        if(hitCollider is BoxCollider)
        {
            BoxCollider box = (BoxCollider) hitCollider;

            transform.position = box.bounds.center;
            transform.rotation = box.transform.rotation;

            ResizeParticles(arcsParticles, Vector3.Scale(box.size, box.transform.localScale));
            ResizeParticles(sparksParticles, Vector3.Scale(box.size, box.transform.localScale));

            currentAnimTime = 0;
            currentBoxVolume = BoxArea(Vector3.Scale(box.size, box.transform.localScale));
        }
    }

    private void ResizeParticles(ParticleSystem particles, Vector3 boxSize)
    {
        var shape = particles.shape;

        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = boxSize;

        particles.Play();
    }

    private void ChangeIntensity(float intensity)
    {
        var lights = sparksParticles.lights;

        ChangeParticlesIntensity(arcsParticles, intensity);
        ChangeParticlesIntensity(sparksParticles, intensity);

        lights.intensityMultiplier = intensity * maxLightIntensity;
        lights.rangeMultiplier = intensity * maxLightRange;
    }

    private void ChangeParticlesIntensity(ParticleSystem particles, float intensity)
    {
        var emission = particles.emission;
        emission.rateOverTime = rateMultiplier * intensity * currentBoxVolume;
    }

    private float BoxArea(Vector3 boxSize)
    {
        return 2 * (boxSize.x * boxSize.y + boxSize.x * boxSize.z + boxSize.y * boxSize.z);
    }
}
