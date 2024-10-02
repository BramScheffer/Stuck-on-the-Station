using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;  // Import TextMeshPro

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;   // Reference to the AudioMixer
    public Slider volumeSlider;     // Reference to the UI Slider
    public TMP_Text volumeText;     // Optional: Text to display volume percentage

    void Start()
    {
        // Load saved volume setting (default to 0 if no saved value)
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        volumeSlider.value = savedVolume;
        audioMixer.SetFloat("MasterVolume", savedVolume);

        // Add listener for slider value changes
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // Update text label if available
        if (volumeText != null)
        {
            UpdateVolumeText(savedVolume);
        }
    }

    // Called when slider value changes
    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("MasterVolume", sliderValue);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);  // Save the volume setting

        // Update volume text if available
        if (volumeText != null)
        {
            UpdateVolumeText(sliderValue);
        }
    }

    // Optional: Update volume percentage text
    private void UpdateVolumeText(float sliderValue)
    {
        volumeText.text = Mathf.RoundToInt((sliderValue + 80) / 80 * 100) + "%";
    }
}
