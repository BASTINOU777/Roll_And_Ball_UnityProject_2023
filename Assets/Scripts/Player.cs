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

    //instancie la variable de type Scene
    Scene _scene;
    

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _scoreText.text = "Score : " + ScoreValue;
        // m�thode SceneManager
        _scene = SceneManager.GetActiveScene();
        // je r�cup tous mes �l�ments
        Debug.Log("La sc�ne s'appelle: " + _scene.name + " et son index est : " + _scene.buildIndex);
    }

    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) 
        {
            _rigidbody.AddForce(Input.GetAxis("Horizontal") * 0.5f, 0f, Input.GetAxis("Vertical") * 0.5f);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target_Trigger"))
        {
            UpdateScore();
            Destroy(other.gameObject);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            UpdateScore();
            Destroy(collision.gameObject);
        }
    }
    private void UpdateScore()
    {
        ScoreValue++;
        PlayerPrefs.SetString("Score", "Score : " + ScoreValue.ToString());
        _scoreText.text = PlayerPrefs.GetString("Score");
        Instantiate(_wallPrefab, _scenario.FirstWalls[0], Quaternion.identity);


        // si le score = 8 alors on change de sc�ne ( niveau suivant )
        if (ScoreValue == 8)
        {
            SceneManager.LoadScene("Level2");
            Debug.Log("Index de la sc�ne active : " + _scene.name);
        }

        // on r�cup le score pour le niveau suivant
        _scenario.Score = ScoreValue;
        // on incr�mente 
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    private void OnDestroy()
    {
        //On supprime la cl� une fois terminer.
    PlayerPrefs.DeleteKey("Score");
        
    }
}
