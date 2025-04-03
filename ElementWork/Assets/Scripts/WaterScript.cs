using Unity.Hierarchy;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public Camera cam;
    public GameObject character;
    public PlayerController pc;
    public GameObject anchor1;
    public GameObject anchor2;
    private bool instantiatedAnchor1 = false;
    private bool instantiatedAnchor2 = false;
    private bool anchor1Locked = false;


    public void Raycast()
    {
        if (!instantiatedAnchor1)
        {
            instantiatedAnchor1 = true;
            anchor1.SetActive(true);
        }
        if (!instantiatedAnchor2 && anchor1Locked)
        {
            instantiatedAnchor2 = true;
            anchor2.SetActive(true);
        }
        Ray ray = cam.ScreenPointToRay(character.transform.position);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 10000f, 3);
        Vector3 temp = hit.point;
        cam.transform.TransformPoint(temp);
        if (anchor1Locked)
        {
            anchor2.transform.position = temp;
        } 
        else
        {
            anchor1.transform.position = temp;
        }
    }

    public void Lock()
    {
        if (anchor1Locked)
        {
            pc.SetRaycastFinished(true);
            pc.Bridge(anchor1, anchor2);
            Deactivate();
        }
        else
        {
            anchor1Locked = true;
            anchor1.transform.LookAt(cam.transform);
            Quaternion flat = Quaternion.Euler(0f, anchor1.transform.rotation.y, 0f);
            anchor1.transform.rotation = flat;
        }
    }

    public void Deactivate()
    {
        anchor1.SetActive (false);
        anchor2.SetActive (false);
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
