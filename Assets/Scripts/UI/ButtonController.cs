using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    // Editor parameters
    public GameObject mainMenu;
    public CanvasGroup menuGroup;

    public AudioSource selectSound;
    public AudioSource clickSound;

    public int clickAnimationMaxExpand;
    public float clickAnimationTime;

    public bool selectByDefault;

    // Refs
    protected Button button;
    protected Text text;

    // State
    protected bool isSelected;

    private void Awake()
    {
        button = GetComponent<Button>();
        text = transform.GetChild(0).GetComponent<Text>();

        button.onClick.AddListener(OnClick);
    }

    public virtual void OnEnable()
    {
        UpdateInteractableLook();

        if (selectByDefault)
        {
            StartCoroutine(SelectOnNextFrameCR());
        }
    }

    public virtual void OnClick()
    {
        clickSound.Play();
    }

    public virtual void OnSelect(BaseEventData eventData)
    {
        isSelected = true;

        SetSelectedLook();
        selectSound.Play();
    }

    public virtual void OnDeselect(BaseEventData eventData)
    {
        SetDefaultLook();
        isSelected = false;
    }

    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
        UpdateInteractableLook();
    }

    private void UpdateInteractableLook()
    {
        if (button.interactable)
        {
            SetDefaultLook();
        }
        else
        {
            SetDisabledLook();
        }
    }

    private void SetDefaultLook()
    {
        text.color = Color.white;
        text.fontStyle = FontStyle.Normal;
    }

    private void SetSelectedLook()
    {
        text.color = Color.black;
        text.fontStyle = FontStyle.Bold;
    }

    private void SetDisabledLook()
    {
        text.color = new Color(1, 1, 1, 0.1f);
        text.fontStyle = FontStyle.Normal;
    }

    private IEnumerator SelectOnNextFrameCR()
    {
        yield return new WaitForEndOfFrame();
        button.Select();
    }
}
