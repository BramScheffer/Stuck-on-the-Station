using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    // Reference to the button
    public Button tmpProButton;

    // Name of the main menu scene
    public string mainMenuSceneName = "MainMenu";

    void Start()
    {
        // Add a listener to call GoToMainMenu when the button is clicked
        tmpProButton.onClick.AddListener(GoToMainMenu);
    }

    // Function to load the main menu scene
    void GoToMainMenu()
    {
        // Reset time scale to 1
        Time.timeScale = 1;

        // Load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
