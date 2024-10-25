using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelFlyInNoButton : MonoBehaviour
{
    public RectTransform panel;             // RectTransform of the UI panel that needs to move
    public Vector3 targetPosition;          // The target position where the panel should end up (on-screen)
    public Button backButton;               // The "Back" button to trigger reverse animation
    public float flyInSpeed = 0.25f;        // Duration for panel movement (lower is faster)
    public bool isPanelVisible = false;     // Tracks whether the panel is currently visible or not
    private Vector3 offScreenPosition;      // Start position of the panel (off-screen to the right)
    private Coroutine movePanelCoroutine;   // Reference to the currently running coroutine
    private Vector3 velocity = Vector3.zero; // SmoothDamp velocity reference

    void Start()
    {
        // Set the start position of the panel off-screen to the right (right side of the screen)
        offScreenPosition = new Vector3(Screen.width, targetPosition.y, 0f);
        panel.anchoredPosition = offScreenPosition;

        // Add listener to the back button only
        backButton.onClick.AddListener(ClosePanel);

        // Hide and lock the cursor initially
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Listen for the Escape key to toggle the panel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel();
        }
    }

    void TogglePanel()
    {
        // Stop any current panel movement coroutine
        if (movePanelCoroutine != null)
        {
            StopCoroutine(movePanelCoroutine);
        }

        if (isPanelVisible)
        {
            // Move the panel off-screen to the right
            movePanelCoroutine = StartCoroutine(MovePanel(offScreenPosition));
        }
        else
        {
            // Move the panel to the target position (visible on-screen)
            movePanelCoroutine = StartCoroutine(MovePanel(targetPosition));
        }

        isPanelVisible = !isPanelVisible;  // Toggle the panel's visibility state
    }

    // Function to handle back button and reverse animation
    void ClosePanel()
    {
        if (isPanelVisible)
        {
            if (movePanelCoroutine != null)
            {
                StopCoroutine(movePanelCoroutine);
            }
            movePanelCoroutine = StartCoroutine(MovePanel(offScreenPosition));
            isPanelVisible = false;  // Set the panel as hidden after moving out
        }
    }

    IEnumerator MovePanel(Vector3 destination)
    {
        while (Vector3.Distance(panel.anchoredPosition, destination) > 0.1f)
        {
            // Smoothly move the panel to the destination using SmoothDamp
            panel.anchoredPosition = Vector3.SmoothDamp(panel.anchoredPosition, destination, ref velocity, flyInSpeed);

            yield return null;  // Wait for the next frame
        }

        // Ensure the panel exactly reaches the destination
        panel.anchoredPosition = destination;
        movePanelCoroutine = null;  // Reset the coroutine reference when done
    }
}
