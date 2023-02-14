using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int ScoreValue = 0;
    public float speed = 2f;
    public float gravity = 20f;
    Vector3  movement = Vector3.zero;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private ScenarioData _scenario;
    //[SerializeField] LayerMask floor;

    //instancie la variable de type Scene
    Scene _scene;
    

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _scoreText.text = "Score : " + ScoreValue;
        // méthode SceneManager
        _scene = SceneManager.GetActiveScene();
        // je récup tous mes éléments
        Debug.Log("La scène s'appelle: " + _scene.name + " et son index est : " + _scene.buildIndex);
        

    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            _rigidbody.AddForce(Input.GetAxis("Horizontal") * 0.5f, 0f, Input.GetAxis("Vertical") * 0.5f);
        }

        if (Input.GetKey(KeyCode.Space))
            
            //|| Physics.CheckSphere(transform.position, floor)
        {
            movement.y += gravity * Time.deltaTime;
            Debug.Log("je suis dans l'espace");
            _rigidbody.AddForce( 0f, 200f, 0f);
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target_Trigger"))
        {
            UpdateScore();
            Destroy(other.gameObject);
            Vector3 position = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-7f, 7f));
            Instantiate(_wallPrefab, position, Quaternion.identity);
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
        ScoreValue++;
        PlayerPrefs.SetString("Score", "Score : " + ScoreValue.ToString());
        _scoreText.text = PlayerPrefs.GetString("Score");

        // si le score = 8 alors on change de scène ( niveau suivant )
        if (ScoreValue == 8)
        {
            SceneManager.LoadScene("Level2");
            Debug.Log("Index de la scène active : " + _scene.name);
        }

        // on récup le score pour le niveau suivant
        _scenario.Score = ScoreValue;
        // on incrémente 
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    private void OnDestroy()
    {
        //On supprime la clé une fois terminer.
    PlayerPrefs.DeleteKey("Score");
        
    }
}
