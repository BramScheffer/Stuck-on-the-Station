using System.Collections;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera playCam;
    public Camera buyCam;
    private Camera activeCamera;
    public bool camswitched;
    public bool inBuy;
    public bool dead = false;
    public GameObject buyscreen;
    public InGameSettingsPanel settingsPanel; // Reference to the settings panel script

    void Start()
    {
        activeCamera = playCam;
        playCam.enabled = true;
        buyCam.enabled = false;
        camswitched = false;
        Cursor.lockState = CursorLockMode.Locked;
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
                playCam.enabled = false;
                buyCam.enabled = true;
                activeCamera = buyCam;
                camswitched = false;
                buyscreen.SetActive(true);
                inBuy = true;

                // Only unlock the cursor if the settings panel is not open
                if (!settingsPanel.IsSettingsPanelVisible())
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }
            }
            else
            {
                playCam.enabled = true;
                buyCam.enabled = false;
                activeCamera = playCam;
                camswitched = false;
                buyscreen.SetActive(false);
                inBuy = false;

                // Only lock the cursor if the settings panel is not open
                if (!settingsPanel.IsSettingsPanelVisible())
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
    }

    public void death()
    {
        playCam.enabled = false;
        buyCam.enabled = true;
        activeCamera = buyCam;
        inBuy = false;
        dead = true;

        // Only unlock the cursor if the settings panel is not open
        if (!settingsPanel.IsSettingsPanelVisible())
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
