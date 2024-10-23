using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameSettingsPanel : MonoBehaviour
{
    public RectTransform panel;
    public Vector3 targetPosition;
    public Button backButton;
    public float flyInSpeed = 2f;
    public bool isPanelVisible = false;
    private Vector3 offScreenPosition;
    private Coroutine movePanelCoroutine;

    void Start()
    {
        // Set the panel's initial off-screen position
        offScreenPosition = new Vector3(Screen.width, targetPosition.y, 0f);
        panel.anchoredPosition = offScreenPosition;
        backButton.onClick.AddListener(ClosePanel);
        ResumeGame();
    }

    void Update()
    {
        // Process Escape key to toggle panel visibility without delay
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel();
        }
    }

    // Toggles the visibility of the settings panel
    void TogglePanel()
    {
        // Stop any currently running panel animation to prevent conflicts
        if (movePanelCoroutine != null)
        {
            StopCoroutine(movePanelCoroutine);
        }

        // Toggle panel visibility immediately
        isPanelVisible = !isPanelVisible;

        if (isPanelVisible)
        {
            // Move panel to on-screen and pause the game
            movePanelCoroutine = StartCoroutine(MovePanel(targetPosition, PauseGame));
        }
        else
        {
            // Move panel off-screen and resume the game
            movePanelCoroutine = StartCoroutine(MovePanel(offScreenPosition, ResumeGame));
        }
    }

    // Closes the panel via the back button
    void ClosePanel()
    {
        if (isPanelVisible)
        {
            // Stop any running animation and move the panel off-screen
            if (movePanelCoroutine != null)
            {
                StopCoroutine(movePanelCoroutine);
            }

            isPanelVisible = false;
            movePanelCoroutine = StartCoroutine(MovePanel(offScreenPosition, ResumeGame));
        }
    }

    // Coroutine to smoothly move the panel from its current position to a destination
    IEnumerator MovePanel(Vector3 destination, System.Action onComplete)
    {
        float duration = 1f / flyInSpeed; // Duration of animation
        float elapsedTime = 0f;          // Time that has passed during animation
        Vector3 startingPosition = panel.anchoredPosition; // Start position of the panel

        while (elapsedTime < duration)
        {
            // Smoothly interpolate between the starting and target position
            panel.anchoredPosition = Vector3.Lerp(startingPosition, destination, elapsedTime / duration);

            // Increment elapsed time by unscaledDeltaTime for smooth animation even when time is paused
            elapsedTime += Time.unscaledDeltaTime;

            yield return null; // Wait for the next frame
        }

        // Ensure the panel exactly reaches the destination
        panel.anchoredPosition = destination;
        movePanelCoroutine = null; // Reset the coroutine reference

        // Call the onComplete action after the animation is done
        onComplete?.Invoke();
    }

    // Pauses the game and makes the cursor visible/unlocked
    void PauseGame()
    {
        // Pause the game AFTER the animation finishes
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Resumes the game and hides/locks the cursor
    void ResumeGame()
    {
        // Resume the game AFTER the panel closes
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
