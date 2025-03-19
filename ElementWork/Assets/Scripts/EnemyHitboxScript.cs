using UnityEngine;

public class EnemyHitboxScript : MonoBehaviour
{
    public FireballScript fbs;

    private void OnTriggerEnter (Collider other)
    {
        // other.gameObject
        // enemy dmg goes here
        Debug.Log("hit");
        fbs.setEarlyDetonate(true);
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
