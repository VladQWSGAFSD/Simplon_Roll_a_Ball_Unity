using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    private int _scoreValue = 0;
    [Tooltip("Adjust the life span of a platforme when player gets in collision with it. Wheater player gets in a collision or not plateform will be distroyed in 10 secs.")]
    [SerializeField] float lifeSpan = 7;
    private Dictionary<GameObject, bool> collidedTargets = new Dictionary<GameObject, bool>();

    [SerializeField] TMP_Text scoreText;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            _scoreValue = PlayerPrefs.GetInt("Score");
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
        PlayerPrefs.SetInt("Score", _scoreValue);
    }

}