using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    private GameObject player;
    private Vector3 offset = new Vector3(0, (float)-8, 9);
    private Quaternion rotation = new Quaternion((float)0.18, 0, 0, 1);

    private void Start()
    {
        player = GameObject.Find("Player");
        target = player.transform;
        transform.rotation = rotation;
    }

    private void Update()
    {
        transform.position = target.position - offset;
        //transform.LookAt(target);
    }
}
