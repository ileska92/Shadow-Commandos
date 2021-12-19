using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    Gamemanager gameManager;

    void LateUpdate()
    {
        /*
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        */
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

