using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // Editor parameters
    public AnimationCurve fadeOut;
    public AnimationCurve fadeIn;

    // Songs
    public AudioSource blackScreenHum;
    public AudioSource mainSong;

    // State
    private bool fading = false;
    private AudioSource from;
    private AudioSource to;
    private float currentTime;

    private void Update()
    {
        if(fading)
        {
            from.volume = fadeOut.Evaluate(currentTime);
            to.volume = fadeIn.Evaluate(currentTime);

            currentTime += Time.deltaTime;

            if(currentTime > fadeOut[fadeOut.length - 1].time && currentTime > fadeIn[fadeIn.length - 1].time)
            {
                fading = false;
                currentTime = 0;
                from.Stop();
            }
        }
    }

    public void FadeInMainMusic()
    {
        SwapSong(blackScreenHum, mainSong);
    }

    private void SwapSong(AudioSource oldAudio, AudioSource newAudio)
    {
        fading = true;
        from = oldAudio;
        to = newAudio;
        currentTime = 0;

        newAudio.volume = 0;
        newAudio.Play();
    }
}
