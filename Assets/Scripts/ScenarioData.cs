using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(menuName = "New Scénario")]

public class ScenarioData : ScriptableObject
{
   
    public Vector3[] FirstWalls;
    public int Score;


    public void OnEnable()
    {
        Score = 0;
    }
}
