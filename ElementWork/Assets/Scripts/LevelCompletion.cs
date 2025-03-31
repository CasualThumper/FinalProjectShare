using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    public Camera camera;
    public CameraController cc;
    public PlayerController pc;
    public Collider machine;
    public Collider water;
    public GameObject endMenuSuccess;
    public GameObject endMenuFail;

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(machine))
        {
            pc.SetInMenu(true);
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            endMenuSuccess.SetActive(false);
            Vector3 v = camera.transform.position;
            endMenuSuccess.transform.position = v;
            endMenuSuccess.transform.LookAt(v);
            endMenuSuccess.SetActive(true);
            cc.SetIsPaused(true);
            Time.timeScale = 0;
        }
        else if (other.Equals(water))
        {
            pc.SetInMenu(true);
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            endMenuFail.SetActive(false);
            Vector3 v = camera.transform.position;
            endMenuFail.transform.position = v;
            endMenuFail.transform.LookAt(v);
            endMenuFail.SetActive(true);
            cc.SetIsPaused(true);
            Time.timeScale = 0;
        }
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
