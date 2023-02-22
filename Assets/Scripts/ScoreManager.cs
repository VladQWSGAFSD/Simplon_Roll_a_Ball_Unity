using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int _scoreValue = 0;
    private int _highScoreValue = 0;
    [Tooltip("Adjust the life span of a platform when player gets in collision with it. Whether player gets in a collision or not, the platform will be destroyed in 10 secs.")]
    [SerializeField] float lifeSpan = 7;
    private float timeRemaining = 10f;
    private Dictionary<GameObject, bool> collidedTargets = new Dictionary<GameObject, bool>();
    [SerializeField] GameObject _particleSystem; // Reference to the particle system
    private bool _particleSystemActivated = false; // Whether the particle system has been activated for this game session

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text timerText;

    private AudioSource _audioSource;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        _scoreValue = 0;
        scoreText.text = "Score: " + _scoreValue;

        _highScoreValue = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + _highScoreValue;
       

        _particleSystem.SetActive(false);
        _particleSystemActivated = false;
        _audioSource = _particleSystem.GetComponent<AudioSource>(); // Get the AudioSource component from the particle system
    }
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        // Update the timer text
        timerText.text = "Time: " + Mathf.Round(timeRemaining);

        if (timeRemaining <= 0)
        {
            Debug.Log("plateform destroyed.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target") && !collidedTargets.ContainsKey(collision.gameObject))
        {
            collidedTargets[collision.gameObject] = true;
            ChangeScore();
            scoreText.text = "Score: " + _scoreValue;
            Destroy(collision.gameObject, lifeSpan);

            // Start the timer
            timeRemaining = lifeSpan;
        }
    }

    private void ChangeScore()
    {
        if (_scoreValue > _highScoreValue)
        {
            _highScoreValue = _scoreValue;
            highScoreText.text = "High Score: " + _highScoreValue;
            PlayerPrefs.SetInt("HighScore", _highScoreValue);

            if (!_particleSystemActivated)
            {
                // Activate the particle system for 5 seconds if score is higher than highscore and the particle system not activated yet
                _particleSystem.SetActive(true);
                _audioSource.PlayDelayed(1.5f);
                StartCoroutine(DeactivateParticleSystem(7f)); 
                _particleSystemActivated = true;
            }


        }
        else
        {
            _scoreValue++;
            scoreText.text = "Score: " + _scoreValue;
        }
    }

    public void PunishScore()
    {
        if (_scoreValue >= 4)
        {
            _scoreValue -= 4;
        }
        else
        {
            _scoreValue = 0;
        }
        scoreText.text = "Score: " + _scoreValue;
    }

    IEnumerator DeactivateParticleSystem(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Deactivate particle system
        _particleSystem.SetActive(false);
    }

}
