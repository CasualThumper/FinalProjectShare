using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour
{

    public void NextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        string path = scene.path;
        int currScene = -1;
        int sceneMax = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneMax; i++)
        {
            if (SceneManager.GetSceneByBuildIndex(i).isLoaded)
            {
                currScene = i;
                break;
            }
        }
        SceneManager.LoadSceneAsync(currScene+1);
    }

    public void RetryLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        string path = scene.path;
        int currScene = -1;
        int sceneMax = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneMax; i++)
        {
            if (SceneManager.GetSceneByBuildIndex(i).isLoaded)
            {
                currScene = i;
                break;
            }
        }
        SceneManager.LoadSceneAsync(currScene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
