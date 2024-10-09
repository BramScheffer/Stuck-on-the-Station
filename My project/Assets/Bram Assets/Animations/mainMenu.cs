using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    
    public Canvas main;
    public Canvas settings;
  public void PlayClicked()
    {
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
