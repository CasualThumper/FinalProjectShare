using UnityEngine;

public class Key : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the key
        if (other.CompareTag("Player"))
        {
            if (KeyManager.instance != null)
            {
                KeyManager.instance.CollectKey();
                Destroy(gameObject); // Destroy the key once collected
            }
            else
            {
                Debug.LogError("KeyManager instance not found! ");
            }
        }
    }
}


