using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;  // Import TextMeshPro

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;   // Reference to the AudioMixer
    public Slider volumeSlider;     // Reference to the UI Slider
    public TMP_Text volumeText;     // Text to display volume percentage

    void Start()
    {
        // Set slider range to match decibel range (-80 dB to 0 dB)
        volumeSlider.minValue = -80f;
        volumeSlider.maxValue = 0f;

        // Load saved volume setting (default to 0 if no saved value)
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        volumeSlider.value = savedVolume;
        audioMixer.SetFloat("MasterVolume", savedVolume);

        // Add listener for slider value changes
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // Update the volume text
        if (volumeText != null)
        {
            UpdateVolumeText(savedVolume);
        }
    }

    // Called when the slider value changes
    public void SetVolume(float sliderValue)
    {
        // Set volume in AudioMixer using slider value (in decibels)
        audioMixer.SetFloat("MasterVolume", sliderValue);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);  // Save the volume setting

        // Update the volume text
        if (volumeText != null)
        {
            UpdateVolumeText(sliderValue);
        }
    }

    // Method to update the volume percentage text
    private void UpdateVolumeText(float sliderValue)
    {
        // Slider value ranges from -80 (mute) to 0 (max volume)
        // Convert to percentage (0% for -80 dB, 100% for 0 dB)
        float percentage = Mathf.Clamp((sliderValue + 80) / 80 * 100, 0, 100);  // Ensure it's between 0 and 100
        volumeText.text = Mathf.RoundToInt(percentage) + "%";  // Round and display as a whole number
    }
}
