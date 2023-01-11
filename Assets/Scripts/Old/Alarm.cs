using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Alarm : MonoBehaviour
{
    // Editor variables
    public bool turnedOn { get; set; }

    public int minBrightness = 150;
    public int maxBrightness = 255;

    public float speed;

    public Renderer[] blikingObjects;
    public AudioSource alarmAudio;

    // Refs
    private Material[] alarmMaterials;


    // Start is called before the first frame update
    void Start()
    {
        alarmMaterials = new Material[blikingObjects.Length];

        for(int i = 0; i < alarmMaterials.Length; i++)
        {
            alarmMaterials[i] = blikingObjects[i].material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(turnedOn)
        {
            float value = Mathf.Cos(2 * Mathf.PI * speed * alarmAudio.time / alarmAudio.clip.length) * 0.5f + 0.5f;
            value = Mathf.SmoothStep(minBrightness, maxBrightness, value) / 255;

            ChangeEmission(new Color(value, 0, 0, 0));
            
            if(!alarmAudio.isPlaying)
            {
                alarmAudio.Play();
            }
        }
        else
        {
            ChangeEmission(new Color(minBrightness / 255f, 0, 0, 0));
            alarmAudio.Stop();
        }
    }

    public void SetAllAlarmsState(bool state)
    {
        foreach(Alarm alarm in FindObjectsOfType<Alarm>())
        {
            alarm.turnedOn = state;
        }
    }

    private void ChangeEmission(Color color)
    {
        for(int i = 0; i < alarmMaterials.Length; i++)
        {
            alarmMaterials[i].SetColor("_EmissionColor", color);
        }
    }
}
