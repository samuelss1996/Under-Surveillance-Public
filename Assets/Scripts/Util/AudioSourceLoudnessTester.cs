using UnityEngine;

public class AudioSourceLoudnessTester : MonoBehaviour
{

    // Editor parameters
    public float updateStep = 0.02f;
    public int sampleDataLength = 1024;

    // Refs
    private AudioSource audioSource;

    // State
    private float currentUpdateTime = 0f;
    private float[] clipSampleData;

    // Shared thingies
    [HideInInspector]
    public float clipLoudness;

    // Use this for initialization
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        clipSampleData = new float[sampleDataLength];
    }

    void Update()
    {
        currentUpdateTime += Time.deltaTime;

        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
            clipLoudness = 0f;

            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }

            clipLoudness /= sampleDataLength;
        }
    }
}