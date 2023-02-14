using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

   

    private void OnEnable()
    {
    
    }

    
    private void OnDisable()
    {
      
    }

   // Score  mis à jour 

     private void UpdateScore(int score)
    {
        scoreText.text = "Score :" + score.ToString();
    }
}
