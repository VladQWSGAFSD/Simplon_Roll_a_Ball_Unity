//First attach this script to a GameObject in the Scene to set up the PlayerPrefs.

using UnityEngine;
using UnityEngine.SceneManagement;

public class SetUpPlayerPrefsExample : MonoBehaviour
{
    string m_PlayerName;

    void Start()
    {
        m_PlayerName = "Enter Your Name";
    }

    void Update()
    {
        //Give the PlayerPrefs some values to send over to the next Scene
        PlayerPrefs.SetFloat("Health", 50.0F);
        PlayerPrefs.SetInt("Score", 20);
        PlayerPrefs.SetString("Name", m_PlayerName);
    }

    void OnGUI()
    {
        //Create a Text Field where the user inputs their name
        m_PlayerName = GUI.TextField(new Rect(10, 10, 200, 20), m_PlayerName, 25);

        //Create a button which loads the appropriate level when you press it
        if (GUI.Button(new Rect(10, 30, 200, 60), "Next Scene"))
        {
            SceneManager.LoadScene("Level 1");
        }
    }
}