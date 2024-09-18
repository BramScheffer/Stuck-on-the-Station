using System.Collections;
using UnityEngine;
using TMPro; // Include the TextMeshPro namespace

public class TextFade : MonoBehaviour
{
    public TextMeshProUGUI text; // Use TextMeshProUGUI for UI text
    public float fadeDuration = 1.0f;

    void Start()
    {
        // Set the alpha of the text to 0 (fully transparent)
        Color color = text.color;
        color.a = 0;
        text.color = color;

        // Start the fade-in coroutine
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
