using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class SettingsMenu : MonoBehaviour
{

    public GameObject graphicsCanvas;
    public GameObject optionsCanvas;
    public Toggle fullscreen;
    private bool fullScreenVal = false;
    private int res = 0;
    public Camera camera;
    public GameObject settingsCanvas;
    public PauseMenuScript pauseMenuScript;
    public TMP_Dropdown dropdown;

    private void Start()
    {
        if (camera != null)
        {
            Vector3 v = camera.transform.position;
            settingsCanvas.transform.position = v;
            settingsCanvas.transform.LookAt(v);
            settingsCanvas.SetActive(false);
        }
    }

    private void Update() { }

    public void OpenGraphics()
    {
        graphicsCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
    }

    public void Back()
    {
        optionsCanvas.SetActive(true);
        // Set all other Canvases to false
        graphicsCanvas.SetActive(false);
    }

    public void SetfullscreenVal()
    {
        this.fullScreenVal = !this.fullScreenVal;
        Debug.Log(fullScreenVal);
    }

    public void SetRes()
    {
        res = dropdown.value;
    }

    public void SaveChanges()
    {
        if(res == 0)
        {
            Screen.SetResolution(1920, 1080, fullScreenVal);
        }
        else if(res == 1){
            Screen.SetResolution(1280, 720, fullScreenVal);
        }
    }

    public void Exit()
    {
        if (settingsCanvas != null)
        {
            settingsCanvas.SetActive(false);
            pauseMenuScript.pauseCanvas.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
