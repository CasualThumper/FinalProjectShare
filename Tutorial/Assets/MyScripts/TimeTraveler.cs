using UnityEngine;
using UnityEngine.UI; // For UI elements
using UnityEngine.SceneManagement; // For loading scenes if needed

public class TIMETraveller : MonoBehaviour
{
    public int requiredKeys = 3; // Number of keys needed to activate the time machine
    public GameObject winPanel;  // Assign the WinPanel in the Inspector

    void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the time machine
        if (other.CompareTag("Player"))
        {
            if (KeyManager.instance != null && KeyManager.instance.keysCollected >= requiredKeys)
            {
                Debug.Log("Time Machine Activated! Level Complete!");
                ShowWinningScreen();
            }
            else
            {
                Debug.Log("You need more keys to activate the time machine.");
            }
        }
    }
    public GameObject winText; // Assign the WinText in the Inspector



    void ShowWinningScreen()
{
    if (winPanel != null)
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f; // Optional: Pause the game
    }
    else
    {
        Debug.LogError("WinPanel is not assigned in the Inspector!");
    }
    if (winText != null)
    {
        winText.SetActive(true); // Show the WinText when the player wins
    }
    else
    {
        Debug.LogError("WinText is not assigned in the Inspector!");
    }
}

}

