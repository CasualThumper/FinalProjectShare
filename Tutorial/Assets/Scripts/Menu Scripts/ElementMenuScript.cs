using UnityEngine;
using UnityEngine.SceneManagement;

public class ElementMenuScript : MonoBehaviour
{

    public GameObject menuCanvas;
    public Camera camera;
    public CameraController cc;

    public void Show()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        menuCanvas.SetActive(false);
        Vector3 v = camera.transform.position;
        menuCanvas.transform.position = v;
        menuCanvas.transform.LookAt(v);
        menuCanvas.SetActive(true);
        cc.setIsPaused(true);
        Time.timeScale = 0.2f;
    }

    public void Hide()
    {
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        menuCanvas.SetActive(true);
        cc.setIsPaused(false);
        menuCanvas.SetActive(false);
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
