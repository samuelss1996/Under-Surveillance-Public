using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCursor : MonoBehaviour
{
    public Texture2D[] cursors;
    private int currentCursor = 0;

    private void Update()
    {
        if(Input.GetKeyDown("c"))
        {
            currentCursor++;

            if(currentCursor >= cursors.Length)
            {
                currentCursor = 0;
            }
        }

        Cursor.SetCursor(cursors[currentCursor], Vector2.zero, CursorMode.Auto);
    }
}
