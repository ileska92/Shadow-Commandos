using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public Texture2D mouseCursor;
    Vector2 hotSpot = new Vector2(0, 0);
    Vector2 reSizeMouse;
    //CursorMode cursorMode = CursorMode.Auto;

    // Start is called before the first frame update
    void Start()
    {
        reSizeMouse = new Vector2(mouseCursor.width * 0.5f, mouseCursor.height * 0.5f);
        hotSpot = reSizeMouse;
        Cursor.SetCursor(mouseCursor, hotSpot, CursorMode.ForceSoftware);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
