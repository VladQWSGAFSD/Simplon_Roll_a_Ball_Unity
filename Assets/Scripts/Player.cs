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
    private int _enemiesToDestroy = 10;
    private float lifeSpan = 3f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] LayerMask groundLayer;

 
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
        scoreText.text = "Score: " + _scoreValue;

    }

    void Update()
    {
        ContinuousJump();
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            _rigidbody.AddForce(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            ChangeScore();
            OnUpdate?.Invoke(_scoreValue);
            scoreText.text = "Score: " + _scoreValue;
          //  OnScene?.Invoke(_destroyedEnemies, _enemiesToDestroy);
            Destroy(collision.gameObject, lifeSpan);
        }

    }
   
    private void ChangeScore()
    {
        _scoreValue++;
        //_destroyedEnemies++;
    }
    private void AddWall()
    {
        Instantiate(scenarioWalls.WallePrefab, scenarioWalls.Walls[_destroyedEnemies].position, scenarioWalls.Walls[_destroyedEnemies].rotation);
    }
    void ContinuousJump()
    {
        if (IsGrounded())
        {
            Vector3 jumpVelocity = _rigidbody.velocity;
            jumpVelocity.y = jumpForce;
            _rigidbody.velocity = jumpVelocity;

        }
    }
    bool IsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.6f, groundLayer))
        {
            return true;
        }
        return false;
        
    }
}

