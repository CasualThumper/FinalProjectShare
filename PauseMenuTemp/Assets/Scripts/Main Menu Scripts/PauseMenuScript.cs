using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject canvas;
    public Camera camera;
    public void Pause()
    {
        Time.timeScale = 0;
        this.Update();
        canvas.SetActive(true);
    }

    public void ResumeGame()
    {
        if (!this.didStart)
        {
            this.Start();
        }
        Time.timeScale = 1;
        canvas.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = camera.transform.position;
        canvas.transform.position = v;
        canvas.transform.LookAt(v);
    }


}
