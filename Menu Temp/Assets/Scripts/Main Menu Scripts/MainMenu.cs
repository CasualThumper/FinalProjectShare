using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("PreviousLevel");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("LevelSelectionMenu");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting...");
        // Temporary
        Application.Quit();
    }
    
}
