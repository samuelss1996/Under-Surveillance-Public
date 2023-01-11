using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerUI : MonoBehaviour
{
    public Text name;
    public Text dialog;
    public GameObject avatar;

    private Character speaker;

    public void SetSpeaker(Character character)
    {
        speaker = character;
        name.text = character.name;
    }

    public void SetText(string text)
    {
        dialog.text = text;
    }

    public bool CompareSpeaker(Character character)
    {
        return speaker == character;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        avatar.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        avatar.SetActive(false);
    }
}