using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;using UnityEngine.SceneManagement;
public class EventManager : MonoBehaviour
{
   // [SerializeField] TMP_Text scoreText;
    private void OnEnable()
    {
        Player.OnUpdate += UpdateScore;
        Player.OnScene += ChangeScene;
    }

    private void OnDisable()
    {
        Player.OnUpdate -= UpdateScore;
        Player.OnScene -= ChangeScene;
    }

    private void UpdateScore(int ScoreValue)
    {
        PlayerPrefs.SetInt("Score", ScoreValue);
     //   scoreText.text = "Score : " + ScoreValue;
    }

    private void ChangeScene(int destroyedEnemies, int requiredEnemies)
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
            requiredEnemies = 8;
        if (destroyedEnemies >= requiredEnemies)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            requiredEnemies += nextSceneIndex + 1;
            PlayerPrefs.SetInt("Require", requiredEnemies);
            SceneManager.LoadScene(nextSceneIndex);
        }

    }
}