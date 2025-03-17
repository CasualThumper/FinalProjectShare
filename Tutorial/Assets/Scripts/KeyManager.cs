using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance; // Singleton instance to access this class globally
    public int keysCollected = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectKey()
    {
        keysCollected++;
        Debug.Log("Keys Collected: " + keysCollected);
    }
}

