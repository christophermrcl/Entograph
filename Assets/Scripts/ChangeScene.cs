using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // The name of the scene to load
    public string sceneName;

    // Function to be called when the button is clicked
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadScene(string referenceName)
    {
        SceneManager.LoadScene(referenceName);
    }
    public void ExitGame()
    {
        // Exit the application
        Application.Quit();
    }
}
