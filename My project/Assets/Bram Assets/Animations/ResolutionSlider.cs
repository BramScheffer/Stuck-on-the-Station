using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Import TextMeshPro

public class ResolutionSlider : MonoBehaviour
{
    public Slider resolutionSlider;       // The slider UI component
    public TMP_Text resolutionLabel;      // TextMeshPro UI element to display the current resolution
    public Button applyButton;            // Button to apply the resolution change
    private Resolution[] resolutions;     // Array to store available screen resolutions
    private Resolution selectedResolution; // The currently selected resolution

    void Start()
    {
        // Get all supported resolutions for the display
        resolutions = Screen.resolutions;

        // Set slider min and max values based on the available resolutions
        resolutionSlider.minValue = 0;
        resolutionSlider.maxValue = resolutions.Length - 1;

        // Set the default slider value to the current screen resolution
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionSlider.value = i;
                UpdateResolutionLabel(i);
                selectedResolution = resolutions[i]; // Set default selected resolution
                break;
            }
        }

        // Add a listener to handle slider value changes
        resolutionSlider.onValueChanged.AddListener(delegate { OnResolutionChange(); });

        // Add listener for apply button click
        applyButton.onClick.AddListener(ApplyResolution);
    }

    // This function is called when the slider value changes
    void OnResolutionChange()
    {
        int index = Mathf.RoundToInt(resolutionSlider.value);  // Get the rounded slider value as the index
        selectedResolution = resolutions[index];               // Store the selected resolution (but don't apply it yet)

        // Update the displayed resolution label
        UpdateResolutionLabel(index);
    }

    // Updates the resolution label text to show the selected resolution
    void UpdateResolutionLabel(int index)
    {
        resolutionLabel.text = resolutions[index].width + " x " + resolutions[index].height;
    }

    // Applies the selected resolution when the "Apply" button is pressed
    void ApplyResolution()
    {
        // Set the screen resolution to the selected resolution
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

        // Optionally, give feedback to the user that the resolution has been applied
        Debug.Log("Resolution applied: " + selectedResolution.width + " x " + selectedResolution.height);
    }
}
