using UnityEngine;

public class SlidePanelController : MonoBehaviour
{
    public Animator panelAnimator; // The Animator attached to the sliding panel.
    private bool isPanelVisible = false;

    // This method will be called when the button is pressed.
    public void TogglePanel()
    {
        if (isPanelVisible)
        {
            // Slide out to the left
            panelAnimator.Play("SlideOut");
        }
        else
        {
            // Slide in from the right
            panelAnimator.Play("SlideIn");
        }
        // Toggle the panel visibility state
        isPanelVisible = !isPanelVisible;
    }
}
