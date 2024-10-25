using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartLevelUI : MonoBehaviour
{
    // Reference to the restart button UI element
    public Button restartButton;

    void Start()
    {
        // Ensure the button has been assigned in the inspector
        if (restartButton != null)
        {
            // Add the RestartScene function as a listener to the button click event
            restartButton.onClick.AddListener(RestartScene);
        }
        else
        {
            Debug.LogWarning("Restart Button not assigned in the Inspector.");
        }
    }

    // Function to restart the current scene
    void RestartScene()
    {
        // Reset time scale to 1 in case it was modified
        Time.timeScale = 1;

        // Reload the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
