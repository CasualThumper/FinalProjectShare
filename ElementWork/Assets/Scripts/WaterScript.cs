using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public Camera cam;
    public Camera tempCam;
    public GameObject tempCharacter;
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
        Vector3 v3 = character.transform.position;
        v3.y += 3f;
        tempCharacter.transform.position = v3;
        tempCam.transform.position = cam.transform.position;
        tempCam.transform.LookAt(tempCharacter.transform.position);
        Vector3 dir = tempCam.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Raycast");
        Physics.Raycast(tempCam.transform.position, dir, out hit, 10000f, mask);
        Vector3 temp = hit.point;
        if (anchor1Locked)
        {
            anchor2.transform.position = temp;
            anchor1.transform.LookAt(temp);
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
            StartCoroutine(pc.Bridge(anchor1, anchor2));
            Deactivate();
        }
        else
        {
            anchor1Locked = true;
            anchor1.transform.LookAt(anchor2.transform);
            //Quaternion flat = Quaternion.Euler(0f, anchor1.transform.rotation.y, 0f);
            //anchor1.transform.rotation = flat;
        }
    }

    public void Reset()
    {
        instantiatedAnchor1 = false;
        instantiatedAnchor2 = false;
        anchor1Locked = false;
    }

    public void Deactivate()
    {
        anchor1.SetActive(false);
        anchor2.SetActive(false);
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
