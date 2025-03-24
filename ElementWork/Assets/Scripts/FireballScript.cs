using System;
using UnityEngine;

public class FireballScript : MonoBehaviour
{

    public PlayerController pc;
    public Component fireball;
    public Component detonate;
    public Camera cam;
    public CharacterController character;

    public void Cast(Vector3 pos, Quaternion rot)
    {
        Component newFireball = Instantiate(fireball, pos, rot);
        Component temp = Instantiate(detonate, pos, rot);
        temp.transform.SetParent(character.transform);

        Vector3 offset = cam.transform.localPosition;
        float x = -offset.x;
        float y = character.transform.position.y + 1f;
        float z = 2f;
        Vector3 final = new Vector3(x, y, z);
        temp.transform.localPosition = final;
        Vector3 world = temp.transform.TransformPoint(final);
        newFireball.transform.position = world;

        Vector3 offset2 = new Vector3(x, y, z+10f);
        final += offset2;
        temp.transform.localPosition = final;
        world = temp.transform.TransformPoint(final);
        newFireball.transform.position = Vector3.MoveTowards(newFireball.transform.position, world, 5f * Time.deltaTime);
        // need to do this over time (update method?)
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
