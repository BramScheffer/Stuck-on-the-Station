using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public Canvas main;
    public Canvas settings;

    // This is called when the script is loaded
    void Start()
    {
        // Make the cursor visible and unlocked when in the main menu
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayClicked()
    {
        // Optional: Hide the cursor and lock it when starting the game
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadScene("Level1");
    }

    public void SettingsClicked()
    {
        main.enabled = false;
        settings.enabled = true;
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
        print("Game Quitted");
    }
}
