using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    // Editor parameters
    public Conversation conversation;

    public GameObject speakerLeft;
    public GameObject speakerRight;

    // Other variables
    [HideInInspector]
    public bool isFinsihed = false;

    // References
    private SpeakerUI speakerLeftUI;
    private SpeakerUI speakerRightUI;

    // State
    private int activeLineIndex = 0;

    private void Start()
    {
        speakerLeftUI = speakerLeft.GetComponent<SpeakerUI>();
        speakerRightUI = speakerRight.GetComponent<SpeakerUI>();

        SetConversation(conversation);
    }

    private void Update()
    {
        if(Input.GetKeyUp("space"))
        {
            AdvanceConversation();    
        }
    }

    public void SetConversation(Conversation newConversation)
    {
        conversation = newConversation;

        speakerLeftUI.SetSpeaker(conversation.speakerLeft);
        speakerRightUI.SetSpeaker(conversation.speakerRight);

        speakerLeftUI.Hide();
        speakerRightUI.Hide();
    }

    public void AdvanceConversation()
    {
        if(activeLineIndex < conversation.lines.Length)
        {
            DisplayLine();
            activeLineIndex++;
        }
        else
        {
            speakerLeftUI.Hide();
            speakerRightUI.Hide();
            activeLineIndex = 0;
            isFinsihed = true;
        }
    }

    private void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        if(speakerLeftUI.CompareSpeaker(character))
        {
            SetDialog(speakerLeftUI, speakerRightUI, line.text);
        }
        else
        {
            SetDialog(speakerRightUI, speakerLeftUI, line.text);
        }
    }

    void SetDialog(SpeakerUI activeUI, SpeakerUI inactiveUI, string text)
    {
        activeUI.SetText(text);
        activeUI.Show();
        inactiveUI.Hide();
    }
}
