using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the GameOver function when the player enters the water
            GameOver();
        }
    }

    private void GameOver()
{
    // Activate Game Over UI
    GameObject.Find("GameOverPanel").SetActive(true);
    // Pause the game
    Time.timeScale = 0f;
}


   public void RestartGame()
{
    // Resume the game
    Time.timeScale = 1f;
    // Reload the current scene
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

}

