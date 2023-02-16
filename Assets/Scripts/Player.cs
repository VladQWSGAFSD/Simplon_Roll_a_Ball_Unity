using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int _scoreValue = 0;
    private int _destroyedEnemies = 0;
    private int _enemiesToDestroy = 10;
    private float lifeSpan = 10f;
    private float movementX, movementY;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] LayerMask groundLayer;

 
    [SerializeField] float speed = 5.0f;
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
        _destroyedEnemies++;
    }
    //private void AddWall()
    //{
    //    Instantiate(scenarioWalls.WallePrefab, scenarioWalls.Walls[_destroyedEnemies].position, scenarioWalls.Walls[_destroyedEnemies].rotation);
    //}
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
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }
    void OnJump()
    {
        Debug.Log("jump");
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0f, movementY);
        _rigidbody.AddForce(movement * speed);
    }


}
