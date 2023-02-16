using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float jumpForce = 200f;
    public float speed = 0;
    private float movementX;
    private float movementY;
    private int ScoreValue = 0;
    private int PlatformPop = -1;



    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private ScenarioData _scenario;
    //[SerializeField] LayerMask floor;

    //instancie la variable de type Scene
    Scene _scene;
    

    void Start()
    {
        //je récupère le Rigidboby du gameObjet
        _rigidbody = GetComponent<Rigidbody>();
        _scoreText.text = "Score : " + ScoreValue;
        // méthode SceneManager
        _scene = SceneManager.GetActiveScene();
        // je récup tous mes éléments
        Debug.Log("La scène s'appelle: " + _scene.name + " et son index est : " + _scene.buildIndex);

}

    void Update()
    {
        //if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        //{
        //    _rigidbody.AddForce(Input.GetAxis("Horizontal") * 0.5f, 0f, Input.GetAxis("Vertical") * 0.5f);
        _rigidbody.AddForce(movementX * 3f, 0f, movementY * 3f);
        //}
        //// si j'appuis sur la touche espace
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    _rigidbody.AddForce(Vector3.up * jumpForce);
        //    Debug.Log("je suis dans l'espace");
        //}     
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
        // pour les gameObjet avec le tag "Platform"
        //if (other.gameObject.CompareTag("Platform"))
        //{
        //    UpdateScore();
        //    Destroy(other.gameObject);
        //    Instantiate(_platformPrefab, _scenario.Platforms[0], Quaternion.identity);
        else if (other.gameObject.CompareTag("Platform"))
        {
            PlatformPop++;
            UpdateScore();
            Destroy(other.gameObject);
            Instantiate(_platformPrefab, _scenario.Platform[PlatformPop], Quaternion.identity);
  
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
        //    }
        //    private groundObjet = GameObject;      // C'est l'objet qui va percuter l'objet où est ce Script
        //static var IsGrounded : boolean = true;

        //function Start()
        //    {
        //        IsGrounded = true;
        //    }

        //    function OnTriggerEnter(objetSol : Collider)
        //    {

        //        if (objetSol.gameObject.tag == "Player")
        //        {
        //            IsGrounded = true;
        //            print("IsGrounded = true");
        //        }
        //    }

        //    function Update()
        //    {

        //        if (Input.GetButtonDown("Submit"))
        //        {
        //            IsGrounded = false;
        //            print("IsGrounded = false");
        //        }
    }
    private void UpdateScore()
    {
        ScoreValue++;
        // je crée un score en string 
        PlayerPrefs.SetString("Score", "Score : " + ScoreValue.ToString());
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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    private void OnDestroy()
    {
        //On supprime la clé une fois terminer.
    //PlayerPrefs.DeleteKey("Score");
        
    }
}
