using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    private GameObject graphics;
    private GameObject options;
    public Toggle fullscreen;
    private bool fullScreenVal = false;
    private int res = 0;

    private void Start()
    {
        graphics = GameObject.Find("Graphics");
        options = GameObject.Find("Options");
    }

    private void Update() { }

    public void OpenGraphics()
    {
        graphics.SetActive(true);
        options.SetActive(false);
    }

    public void Back()
    {
        options.SetActive(true);
        // Set all other Canvases to false
        graphics.SetActive(false);
    }

    public void setfullscreenVal(bool val)
    {
        this.fullScreenVal = val;
    }

    public void setRes(int val)
    {
        this.res = val;
    }

    public void saveChanges()
    {
        if(res == 0)
        {
            Screen.SetResolution(1920, 1080, fullScreenVal);
        }
        else if(res == 1){
            Screen.SetResolution(1280, 720, fullScreenVal);
        }
        else
        {
            Screen.SetResolution(720, 480, fullScreenVal);
        }
    }

}
