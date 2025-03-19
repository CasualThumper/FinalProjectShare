using System;
using System.Collections;
using UnityEngine;

public class FireballScript : MonoBehaviour
{

    public PlayerController pc;
    public GameObject fireball;
    public GameObject detonate;
    public Camera cam;
    public CharacterController character;
    private bool earlyDetonate = false;
    private float elapsedTime = 0f;
    private float duration = 1f;

    public void Cast(Vector3 pos, Quaternion rot)
    {
        if (elapsedTime != 0)
        {
            return;
        }
        GameObject temp = Instantiate(detonate, pos, rot);
        temp.transform.SetParent(character.transform);

        Vector3 offset = cam.transform.localPosition;
        float x = -offset.x;
        float y = character.transform.position.y + 1f;
        float z = 2f;
        Vector3 final = new Vector3(x, y, z);
        temp.transform.localPosition = final;
        Vector3 world = temp.transform.TransformPoint(final);
        GameObject newFireball = Instantiate(fireball, world, rot);

        Vector3 offset2 = new Vector3(0, 0, z+10f);
        final += offset2;
        temp.transform.localPosition = final;
        world = temp.transform.TransformPoint(final);
        StartCoroutine(moveFireball(newFireball, newFireball.transform.position, world));
        // need to do this over time (update method?)
    }

    IEnumerator moveFireball(GameObject newFireball, Vector3 start, Vector3 end)
    {
        while (elapsedTime < duration && !earlyDetonate)
        {
            newFireball.transform.position = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        earlyDetonate = false;
        newFireball.transform.position = end;
        elapsedTime = 0f;
        despawnFireball(newFireball);
    }

    private void despawnFireball(GameObject c)
    {
        Destroy(c);
    }

    public void setEarlyDetonate(bool val)
    {
        earlyDetonate = val;
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
