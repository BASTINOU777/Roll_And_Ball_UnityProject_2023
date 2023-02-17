using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //MOVEMENT
    private Rigidbody _rigidbody;
    public float jumpForce = 200f;
    public float speed = 0;
    private float movementX;
    private float movementY;

    //SCORE
    private int ScoreValue =0;
   

    private int platformIndex = 0;
    



    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private ScenarioData _scenario;

    //instancie la variable de type Scene
    Scene _scene;
    

    void Start()
    {

        //je récupère le Rigidboby du gameObjet
        _rigidbody = GetComponent<Rigidbody>();

        _scoreText.text = PlayerPrefs.GetString("Score");

        // méthode SceneManager.GetActiveScene stocké dans une variable
        //_scene = SceneManager.GetActiveScene();
        // je récup tous mes éléments
        Debug.Log("La scène s'appelle: " + _scene.name + " et son index est : " + _scene.buildIndex);
    }

    void Update()
    {
        //if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        //{
        //    _rigidbody.AddForce(Input.GetAxis("Horizontal") * 0.5f, 0f, Input.GetAxis("Vertical") * 0.5f);
        _rigidbody.AddForce(movementX * 3f, 0f, movementY * 3f);
         
    }

    void OnMove(InputValue movementValue)
    {
        Debug.Log("je suis dans l'espace" + movementValue.Get<Vector2>());
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        _rigidbody.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target_Trigger"))
        {
            Debug.Log("je suis dans  le triger:");
            UpdateScore();
            Destroy(other.gameObject);
            Vector3 position = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-7f, 7f));
            Instantiate(_wallPrefab, position, Quaternion.identity);
        }
        else if (other.gameObject.CompareTag("Platform"))
        {
            UpdateScore();
            Destroy(other.gameObject);
            Instantiate(_platformPrefab, _scenario.Platform[platformIndex], Quaternion.identity);
            platformIndex++;
            if (platformIndex >= _scenario.Platform.Length)
            {
                platformIndex = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            UpdateScore();
            Destroy(collision.gameObject);
            //j'instancie une position random à mon game object du mur (_wallPrefb)
            Vector3 position = new Vector3(Random.Range(-8f, 8f),0f, Random.Range(-7f, 7f));
            Instantiate(_wallPrefab, position, Quaternion.identity);
        }
    }
    private void UpdateScore()
    {
        //j'incrémente
        ScoreValue++;
        // je crée un score en string 
        PlayerPrefs.SetString("Score", "Score : " + ScoreValue);

        // je le récupère 
        _scoreText.text = PlayerPrefs.GetString("Score");

        // si le score = 8 alors on change de scène ( niveau suivant )
        if (ScoreValue == 8)
        {
            SceneManager.LoadScene("Level2");
            Debug.Log("Index de la scène active : " + _scene.name);
        }
        // on récup le score pour le niveau suivant
        //_scenario.Score = ScoreValue;
        // on incrémente 
        //SceneManager.LoadScene();

    }

    // fonction Destroy de MonoBehaviour pour terminer le jeu   
    private void OnDestroy()
    {
        //On supprime la clé une fois terminer.
        PlayerPrefs.DeleteKey("Score");

    }
}
