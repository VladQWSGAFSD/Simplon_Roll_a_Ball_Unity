using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int _scoreValue = 0;
    private int _destroyedEnemies = 0;
    private int _enemiesToDestroy = 2;

    // [SerializeField] private RequiermentsData require;
    [SerializeField] float speed = 1.0f;
    [SerializeField] TMP_Text destroyText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] ScenarioData scenarioWalls;

    #region Events delegate
    public delegate void ScoreEvent(int ScoreValue);
    public static event ScoreEvent OnUpdate;
    public delegate void SceneEvent(int destroyedEnemies, int requiredEnemies);
    public static event SceneEvent OnScene;
    #endregion
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            _scoreValue = PlayerPrefs.GetInt("Score");
            _enemiesToDestroy = PlayerPrefs.GetInt("Require");
        }
        _rigidbody = GetComponent<Rigidbody>();
        destroyText.text = "You need to destroy " + _enemiesToDestroy + " enemies";
        scoreText.text = "Score: " +_scoreValue;

    }

    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) 
        {
            _rigidbody.AddForce(Input.GetAxis("Horizontal") * speed , 0f, Input.GetAxis("Vertical") * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target_Trigger"))
        {
            Destroy(other.gameObject);
            ChangeScore();
            OnUpdate?.Invoke(_scoreValue);
            scoreText.text = "Score: " + _scoreValue;
            OnScene?.Invoke(_destroyedEnemies, _enemiesToDestroy);
            AddWall();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
            ChangeScore();
            OnUpdate?.Invoke(_scoreValue);
            scoreText.text = "Score: " + _scoreValue;
            OnScene?.Invoke(_destroyedEnemies, _enemiesToDestroy);
            AddWall();
        }
    }
    private void ChangeScore()
    {
        _scoreValue++;
        _destroyedEnemies++;
    }
    private void AddWall()
    {
        Instantiate(scenarioWalls.WallePrefab, scenarioWalls.Walls[_destroyedEnemies].position, scenarioWalls.Walls[_destroyedEnemies].rotation);
    }

}
