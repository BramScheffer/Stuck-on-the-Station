using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public GameObject wincond; // Reference to the win condition UI

    // Start is called before the first frame update
    void Start()
    {
        wincond.SetActive(false); // Ensure the win condition UI is hidden at the start
        Cursor.visible = false;   // Optionally, start with the cursor hidden
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor at the start
    }

    // Update is called once per frame
    void Update()
    {
        // Optionally, you can listen for key inputs to restart or exit the game here
    }

    public void restart()
    {
        SceneManager.LoadScene("Level1"); // Load the specified scene
    }

    public void ShowCanvas()
    {
        wincond.SetActive(true); // Show the win condition UI
        Cursor.visible = true;   // Make the cursor visible
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
    }
}
