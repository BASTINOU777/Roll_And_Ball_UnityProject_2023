using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(menuName = "New Scénario")]

public class ScenarioData : ScriptableObject
{
   // sriptableOject pour les murs que je fait 
    public Vector3[] Walls;
    //public Vector3[] Platforms;
    public List<Vector3> position;



    public int Score;


    public void OnEnable()
    {
        Score = 0;
    }
}
