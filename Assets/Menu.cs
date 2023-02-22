using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartScreen()
    {
        SceneManager.LoadScene("Level1");
        Debug.Log("je suis dans chargement");
    }

    //public void Scene1()
    //{
    //    SceneManager.LoadScene("StartScreen 1");
        
    //}
}

