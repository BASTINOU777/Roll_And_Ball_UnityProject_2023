using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    //MOVEMENT
    private Rigidbody _rigidbody;
    public float speed = 0;
    private float movementX;
    private float movementY;
    private bool _touchGround;

    //SCORE
    private int ScoreValue =0;

    //AUDIO
    private AudioSource _audioPlayer;

    //GAME OBJET
    private int platformIndex = 0;


    //j'instancie la variable de type Scene
    Scene _scene;

    [SerializeField] public float jumpForce = 200f;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private ScenarioData _scenario; 
    [SerializeField] private LayerMask LayerGround = -1;
    [SerializeField] private AudioClip _audioJump = null;
    [SerializeField] private GameObject _verifTouchGround = null;
    [SerializeField] private float _distanceOfGround = 0.1f;



    void Start()
    {

        //je récupère le Rigidboby du gameObjet
        _rigidbody = GetComponent<Rigidbody>();

        //je récupère mon composant AudioSource
        _audioPlayer = GetComponent<AudioSource>();

        _scoreText.text = PlayerPrefs.GetString("Score");
        ScoreValue = PlayerPrefs.GetInt("ScoreValue");

        // je vérifie sur quelle scène je suis
        Debug.Log("La scène s'appelle: " + _scene.name + " et son index est : " + _scene.buildIndex);
    }

    void Update()
    {
        // déplacement au joystick
        _rigidbody.AddForce(movementX * 3f, 0f, movementY * 3f);
        //je vérifie si le personnage touche le sol avec un GameObject qui est au pied du personnage.
        //_touchGround = Physics.CheckSphere( _verifTouchGround.position, _distanceOfGround, LayerGround );

    }

    void OnMove(InputValue movementValue)
    {
        Debug.Log("je suis dans l'espace" + movementValue.Get<Vector2>());
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void OnJump(InputValue jumpValue)
    { 
        Debug.Log("je saute");
        _rigidbody.AddForce(new Vector3(0f,jumpForce, 0f));
        // cela me permet de jouer le son que je demande une seule fois 
        _audioPlayer.PlayOneShot(_audioJump);

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
        // je modifie et récupère le score avec la key
        PlayerPrefs.SetString("Score", "Score : " + ScoreValue);
        PlayerPrefs.GetInt("ScoreValue" + ScoreValue);
        PlayerPrefs.SetInt("ScoreValue", ScoreValue);
        _scoreText.text = PlayerPrefs.GetString("Score");

        // si le score = 8 alors on change de scène ( niveau suivant )
        if (ScoreValue == 8)
        {
            SceneManager.LoadScene("Level2");
            Debug.Log("Index de la scène active : " + _scene.name);
        }
    }
    //fonction quand on quitte le jeu 
    private void OnApplicationQuit()
    {
        // je supprime le score
        PlayerPrefs.DeleteKey("ScoreValue");
        PlayerPrefs.DeleteKey("Score");

    }

    // fonction Destroy de MonoBehaviour pour terminer le jeu   
    private void OnDestroy()
    {
        //On supprime la clé une fois terminer.
        //PlayerPrefs.DeleteKey("Score");

    }
}
