using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseCanvas;
    public Camera camera;
    public CameraController cc;
    public GameObject settingsCanvas;
    public void Pause()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        pauseCanvas.SetActive(false);
        Vector3 v = camera.transform.position;
        pauseCanvas.transform.position = v;
        pauseCanvas.transform.LookAt(v);
        pauseCanvas.SetActive(true);
        cc.setIsPaused(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        pauseCanvas.SetActive(true);
        cc.setIsPaused(false);
        pauseCanvas.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    public void Exit()
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
