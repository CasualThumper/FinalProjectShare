using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LowLevelPhysics;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform pivot;
    private GameObject player;
    public bool useOffsetValues;
    private Vector3 offset = new Vector3(0, 0, 0);
    //private Quaternion rotation = new Quaternion((float)0.18, 0, 0, 1);
    public float rotateSpeed;

    private void Start()
    {
        //player = GameObject.Find("Player");
        //target = player.transform;
        //transform.rotation = rotation;
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        transform.position = target.transform.position;

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        Cursor.lockState = CursorLockMode.Locked;

    }

    private void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(vertical, 0, 0);

        if(pivot.rotation.eulerAngles.x > 45f && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(45f, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(315f, 0, 0);
        }

        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        //transform.position = target.position - offset;

        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y -.5f, transform.position.z);
        }

        transform.LookAt(target);
    }
}
