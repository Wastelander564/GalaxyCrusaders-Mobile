using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import SceneManager

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName; // Set this in the Inspector or pass it dynamically

    // Method to load a scene by name
    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // Method to load the assigned scene from Inspector
    public void LoadAssignedScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    // Method to reload the current scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Werkt in de Unity Editor
        #else
            Application.Quit(); // Werkt in een standalone build
        #endif
    }

}
