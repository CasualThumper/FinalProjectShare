using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Drag the pause menu Canvas here
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Check if the Escape key is pressed
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Resume the game time
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Show the pause menu
        Time.timeScale = 0f; // Pause the game time
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Ensure game time is restored
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the game (only works in built application)
    }
}
