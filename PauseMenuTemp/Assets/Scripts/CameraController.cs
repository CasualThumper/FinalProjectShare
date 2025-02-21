// Imports
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LowLevelPhysics;

public class CameraController : MonoBehaviour
{
    // Normal variables
    private Vector3 offset = new Vector3(0, 0, 0);
    public float rotateSpeed;
    private float maxViewAngle = 65f;
    private float minViewAngle = -60f;

    // Special variables; transform is a data type that stores the 3 main components of a transformation in 3 dimensional space: position, rotation, and scale 
    public Transform target;
    public Transform pivot;

    private void Start()
    {
        // NOTE: Whenever transform is referenced without a preceding transformation, the "this" keyword is implied

        // Create the offset between the camera position and the player position
        offset = target.position - transform.position;

        // Temporarily set the camera position equal to the player position
        transform.position = target.transform.position;

        // "Instantiate" the pivot transformation that will be used as a bases for camera rotations
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        // Locks the cursor to the game window
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void LateUpdate()
    {
        // Gets the X and Y-axis values from the mouse and rotates the transformations accordingly
        // NOTE: the target is referenced here because we want the character to move in accordance with the mouse left / right; camera auto-adjusts
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        // NOTE: pivot is referenced here because we want the camera to move vertically, but we do not want the player to move as well
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        /* Pain
         * Long story short: quaternions are a form of rotational calculation that you basically have to have a PHD in mathematics to understand, but Unity uses them by default
         * this is why we convert the rotations into Euler Angles using the respective property; Euler Angles use and easier to understand and manipulate format of independent X, Y, and Z rotations
         * however, it is also prone to an issue known as Gimbal Lock which is very, very, difficult to explain in text or verbally; however, for the simplistic testing and modification that we
         * do here, it is relatively harmless; what this actually does is tests for out-of-bounds rotations and restricts them accordingly
         */
        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        // Creating a new Quaternion using a Euler Angle that uses the x and y components we want for the rotation
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);

        // Changes the camera position in accordance with the target and our new Quaternion
        transform.position = target.position - (rotation * offset);

        // Makes sure the camera cannot go below the player, IE camera cannot go through the ground or get super close to player
        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y -.5f, transform.position.z);
        }

        // Centers the camera's focus on the player
        transform.LookAt(target);
    }
}
