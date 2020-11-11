using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        //This line is to make the quit work in the editor
        UnityEditor.EditorApplication.isPlaying = false;

        Debug.Log("QUIT!");
        Application.Quit();
    }
}
