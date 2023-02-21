using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartScreen()
    {
        SceneManager.LoadScene("Level - 1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Scene1()
    {
        SceneManager.LoadScene("StartScreen 1");
    }
}

