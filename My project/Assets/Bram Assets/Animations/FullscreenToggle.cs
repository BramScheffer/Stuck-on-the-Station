using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FullScreenToggle : MonoBehaviour
{
    public Toggle fullScreenToggle;        // The Toggle UI element
    public TextMeshProUGUI toggleLabel;    // Optional label for showing fullscreen/windowed status

    void Start()
    {
        // Set the initial state of the toggle based on current fullscreen mode
        fullScreenToggle.isOn = Screen.fullScreen;

        // Add a listener to the toggle's value change event
        fullScreenToggle.onValueChanged.AddListener(delegate { ToggleFullScreen(fullScreenToggle.isOn); });

        // Update the label text
        UpdateToggleLabel(fullScreenToggle.isOn);
    }

    // Toggle fullscreen mode based on the toggle's value
    public void ToggleFullScreen(bool isFullScreen)
    {
        // Toggle fullscreen mode
        Screen.fullScreen = isFullScreen;

        // Update the label to reflect the correct mode
        UpdateToggleLabel(isFullScreen);
    }

    // Update the toggle label to show "Fullscreen" or "Windowed"
    void UpdateToggleLabel(bool isFullScreen)
    {
        if (toggleLabel != null)
        {
            // If fullscreen is enabled, show "Fullscreen", otherwise show "Windowed"
            toggleLabel.text = isFullScreen ? "Fullscreen" : "Windowed";
        }
    }
}
