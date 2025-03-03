using UnityEngine;
using UnityEngine.SceneManagement; // For reloading the scene

public class PlayerFallHandler : MonoBehaviour
{
    public float fallThreshold = -10f; // Y position threshold for falling
    public GameObject failPanel;       // Assign a fail panel in the Inspector

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            ShowFailScreen();
        }
    }

    void ShowFailScreen()
    {
        if (failPanel != null)
        {
            failPanel.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Debug.LogError("FailPanel is not assigned in the Inspector!");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
