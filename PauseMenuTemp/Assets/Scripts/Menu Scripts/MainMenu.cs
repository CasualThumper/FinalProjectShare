using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

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

    public void CloseSettings()
    {
        SceneManager.LoadScene("LastScene");
    }

    public void OpenPauseMenu()
    {
        SceneManager.LoadScene("PauseMenu");
    }

    public void ClosePauseMenu()
    {
        SceneManager.LoadScene("LastScene");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
    
}
