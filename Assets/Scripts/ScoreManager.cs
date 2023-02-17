using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    private int _scoreValue = 0;
    private float lifeSpan = 10;
    private Dictionary<GameObject, bool> collidedTargets = new Dictionary<GameObject, bool>();

    [SerializeField] TMP_Text scoreText;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            _scoreValue = PlayerPrefs.GetInt("Score");
        }
        scoreText.text = "Score: " + _scoreValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target") && !collidedTargets.ContainsKey(collision.gameObject))
        {
          
                collidedTargets[collision.gameObject] = true;
                ChangeScore();
                scoreText.text = "Score: " + _scoreValue;
                Destroy(collision.gameObject, lifeSpan);
            
            

        }
    }

    private void ChangeScore()
    {
        _scoreValue++;
        PlayerPrefs.SetInt("Score", _scoreValue);
    }
}