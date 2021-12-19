using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public Texture2D mouseCursor;
    Vector2 hotSpot = new Vector2(0, 0);
    Vector2 reSizeMouse;

    private void Awake()
    {
        SetGameCursor();
    }

    public void Start()
    {
        
        /*
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        */
    }

    public void Update()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "UI")
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        //SceneManager.GetActiveScene().name.Contains("Level")
        if (scene.name == "Level")
        {
            SetGameCursor();
        }

        if(PauseMenu.GameIsPaused == true)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    public void SetGameCursor()
    {
        reSizeMouse = new Vector2(mouseCursor.width * 0.5f, mouseCursor.height * 0.5f);
        hotSpot = reSizeMouse;
        Cursor.SetCursor(mouseCursor, hotSpot, CursorMode.ForceSoftware);
    }
}
