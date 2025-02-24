using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject canvas;
    public Camera camera;
    public CameraController cc;
    public void Pause()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        canvas.SetActive(false);
        Vector3 v = camera.transform.position;
        canvas.transform.position = v;
        canvas.transform.LookAt(v);
        canvas.SetActive(true);
        cc.setIsPaused(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        canvas.SetActive(true);
        cc.setIsPaused(false);
        canvas.SetActive(false);
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
