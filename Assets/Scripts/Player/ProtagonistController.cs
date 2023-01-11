using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistController : MonoBehaviour
{
    // Editor parameters
    public GameObject root;
    public AudioSource swapSound;

    // References
    private Animator animator;
    private UIMenuController menu;

    // State
    [HideInInspector]
    public float axis;
    [HideInInspector]
    public int direction;

    private int currentPowerIndex;
    private List<ProtagonistPowerController> availablePowers;

    void Awake()
    {
        foreach (ProtagonistPowerController power in GetComponents<ProtagonistPowerController>())
        {
            power.enabled = false;
        }

        animator = GetComponent<Animator>();
        menu = FindObjectOfType<UIMenuController>();

        currentPowerIndex = -1;
        availablePowers = new List<ProtagonistPowerController>();
    }

    private void Update()
    {
        axis = !menu.IsMenuActive()? Input.GetAxis("Horizontal") : 0;

        animator.SetFloat("Velocity", direction * axis);
        animator.SetFloat("AbsVelocity", Mathf.Abs(axis));
    }

    private void OnMouseDown()
    {
        if(!menu.IsMenuActive())
        {
            NextPower();

            if (availablePowers.Count > 1)
            {
                swapSound.Play();
            }
        }
    }

    public Vector3? MouseRayCastEnvironment()
    {
        RaycastHit? hit = MouseRayCast();

        if (hit != null && !((RaycastHit)hit).transform.gameObject.CompareTag("Player"))
        {
            return ((RaycastHit)hit).point;
        }

        return null;
    }

    public RaycastHit? MouseRayCast()
    {
        if(!menu.IsMenuActive())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50f))
            {
                return hit;
            }
        }

        return null;
    }

    public ProtagonistPowerController CurrentPower()
    {
        if(currentPowerIndex >= 0)
        {
            return availablePowers[currentPowerIndex];
        }

        return null;
    }

    public void NextPower()
    {
        foreach(ProtagonistPowerController power in availablePowers)
        {
            power.enabled = false;
        }

        if(availablePowers.Count > 0)
        {
            currentPowerIndex++;
            currentPowerIndex %= availablePowers.Count;

            availablePowers[currentPowerIndex].enabled = true;
        }
    }

    public void OnInitialAnimationFinished()
    {
        AddElectricPower();
        NextPower();
    }

    public void AddElectricPower()
    {
        AddPower(GetComponent<ProtagonistElectricityController>());
    }

    public void AddWaterPower()
    {
        AddPower(GetComponent<ProtagonistWaterController>());
    }

    public int PowerCount()
    {
        return availablePowers.Count;
    }

    private void AddPower(ProtagonistPowerController power)
    {
        if (!availablePowers.Contains(power))
        {
            availablePowers.Add(power);
        }
    }
}
