using System.Collections;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
    public TextMeshProUGUI text; // Reference to the TextMeshProUGUI component
    public float fadeDuration = 1.0f; // Duration of the fade effect

    void OnEnable()
    {
        // When the GameObject is enabled, start with fully transparent text
        Color color = text.color;
        color.a = 0;
        text.color = color;

        // Start fading in the text
        StartCoroutine(FadeTextIn());
    }

    IEnumerator FadeTextIn()
    {
        float elapsedTime = 0f;
        Color color = text.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration); // Update the alpha value
            text.color = color;
            yield return null;
        }
    }
}
