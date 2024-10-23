// InGameSettingsPanel.cs
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
        offScreenPosition = new Vector3(Screen.width, targetPosition.y, 0f);
        panel.anchoredPosition = offScreenPosition;
        backButton.onClick.AddListener(ClosePanel);
        ResumeGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel();
        }
    }

    public bool IsSettingsPanelVisible()
    {
        return isPanelVisible;
    }

    void TogglePanel()
    {
        if (movePanelCoroutine != null)
        {
            StopCoroutine(movePanelCoroutine);
        }

        isPanelVisible = !isPanelVisible;

        if (isPanelVisible)
        {
            movePanelCoroutine = StartCoroutine(MovePanel(targetPosition, PauseGame));
        }
        else
        {
            movePanelCoroutine = StartCoroutine(MovePanel(offScreenPosition, ResumeGame));
        }
    }

    void ClosePanel()
    {
        if (isPanelVisible)
        {
            if (movePanelCoroutine != null)
            {
                StopCoroutine(movePanelCoroutine);
            }

            isPanelVisible = false;
            movePanelCoroutine = StartCoroutine(MovePanel(offScreenPosition, ResumeGame));
        }
    }

    IEnumerator MovePanel(Vector3 destination, System.Action onComplete)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = panel.anchoredPosition;

        while (elapsedTime < 1f)
        {
            panel.anchoredPosition = Vector3.Lerp(startingPosition, destination, elapsedTime);
            elapsedTime += Time.unscaledDeltaTime * flyInSpeed;
            yield return null;
        }

        panel.anchoredPosition = destination;
        movePanelCoroutine = null;
        onComplete?.Invoke();
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
