using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;  // For handling UI components

public class LoadSceneOnClick : MonoBehaviour
{
    public string sceneName; // Name of the scene you want to load
    public TMP_Text buttonText; // Reference to the TextMeshPro text on the button
    public Button button; // Reference to the Button component

    void Start()
    {
        if (button != null)
        {
            // Add a listener to the button to detect when it is clicked
            button.onClick.AddListener(OnButtonClick);
        }
    }

    // This method is called when the button is clicked
    void OnButtonClick()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
}
