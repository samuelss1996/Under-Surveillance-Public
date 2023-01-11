using UnityEngine;

public class ProtagonistCursorController : MonoBehaviour
{
    // Editor parameters
    public Texture2D electricCursor;
    public Texture2D interactCursor;
    public Texture2D waterCursor;
    public Texture2D wrongCursor;
    public Texture2D questionCursor;
    public Texture2D swapCursor;

    // Refs
    private ProtagonistController protagonist;
    private UIMenuController menu;

    private void Awake()
    {
        protagonist = GetComponent<ProtagonistController>();
        menu = FindObjectOfType<UIMenuController>();
    }

    private void Update()
    {
        RaycastHit? hitNullable = protagonist.MouseRayCast();

        if (hitNullable != null)
        {
            RaycastHit hit = (RaycastHit)hitNullable;

            if (hit.transform.CompareTag("Player"))
            {
                if (protagonist.PowerCount() > 1)
                {
                    SetCursor(swapCursor);
                }
                else if(FindObjectOfType<HeadFollowMouse>().IsSatDown())
                {
                    SetCursor(questionCursor);
                }
                else
                {
                    SetCursor(wrongCursor);
                }
            }
            else if (protagonist.CurrentPower() is ProtagonistElectricityController)
            {
                bool interactable = hit.transform.gameObject.GetComponent<AElectricityReact>()?.IsInteractable() ?? false;
                SetCursor(interactable ? interactCursor : electricCursor);
            }
            else if(protagonist.CurrentPower() is ProtagonistWaterController)
            {
                SetCursor(waterCursor);
            }
            else
            {
                SetCursor(wrongCursor);
            }
        }
        else if(menu.IsMenuActive())
        {
            SetCursor(null);
        }
        else
        {
            SetCursor(wrongCursor);
        }
    }

    private void SetCursor(Texture2D cursor)
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
}