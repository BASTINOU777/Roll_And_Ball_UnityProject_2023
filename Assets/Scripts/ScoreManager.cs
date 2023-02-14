using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    // affiche le score 

    private void OnEnable()
    {
        Player.OnScoreUpdate += UpdateScore;
    }

    // désactive le score 
    private void OnDisable()
    {
        Player.OnScoreUpdate -= UpdateScore;
    }

// Score à jour 

     private void UpdateScore(int score)
    {
        scoreText.text = "Score :" + score.ToString();
    }
}
