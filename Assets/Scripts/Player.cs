using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int ScoreValue = 0;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private ScenarioData _scenario;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _scoreText.text = "Score : " + ScoreValue;
    }

    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) 
        {
            _rigidbody.AddForce(Input.GetAxis("Horizontal") * 0.5f, 0f, Input.GetAxis("Vertical"));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target_Trigger"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
        }
    }
    private void UpdateScore()
    {
        ScoreValue++;
        PlayerPrefs.SetString("Score", "Score : " + ScoreValue.ToString());
        _scoreText.text = PlayerPrefs.GetString("Score");
        Instantiate(_wallPrefab, _scenario.FirstWalls[0], Quaternion.identity);


        // si le score = 8 alors on passe au niveau suivant
        if (ScoreValue == 8)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // on récup le score pour le niveau suivant
        _scenario.Score = ScoreValue;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
