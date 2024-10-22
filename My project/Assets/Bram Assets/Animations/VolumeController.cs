using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Text volumeText;
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        UpdateVolumeText(volumeSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    public void OnVolumeChanged(float value)
    {
        UpdateVolumeText(value);
        AudioListener.volume = value;
    }

    public void UpdateVolumeText(float value)
    {
        volumeText.text = $"Volume: {Mathf.RoundToInt(value * 100)}%";
    }
}