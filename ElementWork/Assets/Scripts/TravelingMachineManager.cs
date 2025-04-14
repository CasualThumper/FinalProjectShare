using UnityEngine;
using UnityEngine.UI;

public class TravelingMachineManager : MonoBehaviour
{
    public GameObject[] keys; // Assign key objects in the scene
    public GameObject travelingMachine; // Assign the time machine object
    public Text keyCountText; // Assign a UI Text element to display collected keys
    public GameObject winMessage; // Assign a UI message for when the machine is activated

    private int keysCollected = 0;

    void Start()
    {
        // Initialize key count text
        UpdateKeyCount();
        // Ensure the machine is inactive at the start
        travelingMachine.SetActive(false);
        winMessage.SetActive(false);
    }

    void Update()
    {
        // Check if all keys are collected
        if (keysCollected == keys.Length)
        {
            ActivateTravelingMachine();
        }
    }

    public void CollectKey(GameObject key)
    {
        keysCollected++;
        UpdateKeyCount();
        Destroy(key); // Remove the key from the scene
    }

    void UpdateKeyCount()
    {
        keyCountText.text = "Keys Collected: " + keysCollected + " / " + keys.Length;
    }

    void ActivateTravelingMachine()
    {
        Debug.Log("Traveling Machine Activated!");
        travelingMachine.SetActive(true); // Activate the time machine
        winMessage.SetActive(true); // Display the win message
    }
}

