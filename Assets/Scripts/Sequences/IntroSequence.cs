using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSequence : MonoBehaviour
{
    // Editor parameters
    public GameObject blackOverlay;
    public DialogUI dialogUI;
    public AnimationCurve alphaCurve;

    // State
    private int state = 0;
    private float time = 0;

    private void Start()
    {
        blackOverlay.GetComponent<Image>().color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case 0:
                dialogUI.AdvanceConversation();
                state++;
                break;
            case 1:
                if(dialogUI.isFinsihed)
                {
                    state++;
                    GameObject.FindGameObjectWithTag("BGMManager").GetComponent<BGMManager>().FadeInMainMusic();
                }
                break;
            case 2:
                time += Time.deltaTime;

                if(time >= alphaCurve[alphaCurve.length - 1].time)
                {
                    time = alphaCurve[alphaCurve.length - 1].time;
                    state++;
                }

                blackOverlay.GetComponent<Image>().color = new Color(0, 0, 0, alphaCurve.Evaluate(time));
                break;
        }
    }
}
