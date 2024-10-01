using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Add this to use TextMeshPro

public class ResolutionSlider : MonoBehaviour
{
    public Slider resolutionSlider;    // The slider UI component
    public TMP_Text resolutionLabel;   // TextMeshPro UI element to display the current resolution
    private Resolution[] resolutions;  // Array to store available screen resolutions

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
                break;
            }
        }

        // Add a listener to handle slider value changes
        resolutionSlider.onValueChanged.AddListener(delegate { OnResolutionChange(); });
    }

    // This function is called when the slider value changes
    void OnResolutionChange()
    {
        int index = Mathf.RoundToInt(resolutionSlider.value);  // Get the rounded slider value as the index
        Resolution selectedResolution = resolutions[index];    // Get the corresponding resolution

        // Set the screen resolution to the selected resolution
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

        // Update the displayed resolution label
        UpdateResolutionLabel(index);
    }

    // Updates the resolution label text to show the selected resolution
    void UpdateResolutionLabel(int index)
    {
        resolutionLabel.text = resolutions[index].width + " x " + resolutions[index].height;
    }
}
