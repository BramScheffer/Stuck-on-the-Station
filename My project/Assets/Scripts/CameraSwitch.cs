using System.Collections;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera playCam;                       // Reference to the gameplay camera
    public Camera buyCam;                        // Reference to the buy camera
    private Camera activeCamera;                 // The currently active camera
    public bool camswitched;                     // Flag to track if the camera is switched
    public bool inBuy;                           // Flag to track if in the buy screen
    public bool dead = false;                    // Flag to track if the player is dead
    public GameObject buyscreen;                 // The buy screen UI
    public GameObject loseCanvas;                // The lose canvas UI to be activated on death
    public InGameSettingsPanel settingsPanel;    // Reference to the settings panel script

    void Start()
    {
        activeCamera = playCam;
        playCam.enabled = true;
        buyCam.enabled = false;
        camswitched = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Start with the cursor hidden
        buyscreen.SetActive(false);
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // Detects a single press of the "G" key
        {
            if (!camswitched)
            {
                camswitched = true;
                SwitchCamera();
            }
        }
    }

    public void SwitchCamera()
    {
        if (!dead)
        {
            if (activeCamera == playCam)
            {
                // Switching to buyCam
                playCam.enabled = false;
                buyCam.enabled = true;
                activeCamera = buyCam;
                camswitched = false;
                buyscreen.SetActive(true);
                inBuy = true;

                // Unlock and show cursor when entering buy screen
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                // Switching back to playCam
                playCam.enabled = true;
                buyCam.enabled = false;
                activeCamera = playCam;
                camswitched = false;
                buyscreen.SetActive(false);
                inBuy = false;

                // Lock and hide cursor when exiting buy screen
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void death()
    {
        // When the player is "dead," switch to buyCam and show cursor
        playCam.enabled = false;
        buyCam.enabled = true;
        activeCamera = buyCam;
        inBuy = false;
        dead = true;

        // Show the lose canvas
        loseCanvas.SetActive(true);

        // Show cursor when dead regardless of settings panel
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
