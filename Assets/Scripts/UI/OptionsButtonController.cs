using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsButtonController : ButtonController
{
    // Editor parameters
    public GameObject badgeGroup;

    public string[] options;
    public int selected;

    public Color[] activeBadgeColor;
    public Color[] inactiveBadgeColor;

    public ButtonController enableOnChage;

    // References
    private GameObject[] badges;

    // State
    private bool loaded  = false;

    public override void OnEnable()
    {
        base.OnEnable();
        RefreshOptions();
    }

    public override void OnClick()
    {
        selected++;

        if(selected >= options.Length)
        {
            selected = 0;
        }

        enableOnChage.SetInteractable(true);
        clickSound.Play();

        RefreshView();
    }

    public void RefreshOptions()
    {
        badges = new GameObject[options.Length];
        badges[0] = badgeGroup.transform.GetChild(0).gameObject;

        for (int i = 1; i < badges.Length; i++)
        {
            badges[i] = Instantiate(badges[0], badgeGroup.transform);
        }

        loaded = true;

        RefreshView();
    }

    public void RefreshView()
    {
        if(loaded)
        {
            text.text = options[selected];

            foreach (GameObject badge in badges)
            {
                badge.GetComponent<Image>().color = inactiveBadgeColor[isSelected ? 1 : 0];
            }

            badges[selected].GetComponent<Image>().color = activeBadgeColor[isSelected ? 1 : 0];
        }
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        RefreshView();
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        RefreshView();
    }

    private void OnDisable()
    {
        loaded = false;

        for (int i = 1; i < badges.Length; i++)
        {
            DestroyImmediate(badges[i]);
        }
    }
}
