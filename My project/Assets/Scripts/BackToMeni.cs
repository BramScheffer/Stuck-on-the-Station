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
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
