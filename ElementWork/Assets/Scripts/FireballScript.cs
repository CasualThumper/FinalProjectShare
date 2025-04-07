using System.Collections;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public PlayerController pc;
    public GameObject fireball;
    public GameObject detonate;
    public Camera cam;
    public GameObject player;
    private Animator animator;

    private bool earlyDetonate = false;
    private float elapsedTime = 0f;
    private readonly float duration = 1f;

    public void Cast(Vector3 pos, Quaternion rot)
    {
        if (elapsedTime != 0)
        {
            return;
        }
        GameObject temp = Instantiate(detonate, pos, rot);
        GameObject tempParent = Instantiate(detonate, pos, rot);
        temp.transform.SetParent(tempParent.transform);

        Vector3 offset = cam.transform.localPosition;
        float x = offset.x;
        float y = 1f;
        float z = 2f;
        Vector3 final = new(x, y, z);
        temp.transform.localPosition = final;
        Vector3 world = temp.transform.TransformPoint(final);
        GameObject newFireball = Instantiate(fireball, world, rot);
        animator = newFireball.GetComponent<Animator>();

        Vector3 offset2 = new(0, 0, z+10f);
        final += offset2;
        temp.transform.localPosition = final;
        Vector3 world2 = temp.transform.TransformPoint(final);
        Destroy(temp);
        Destroy(tempParent);
        StartCoroutine(MoveFireball(newFireball, world, world2));
    }

    IEnumerator MoveFireball(GameObject newFireball, Vector3 start, Vector3 end)
    {
        while (elapsedTime < duration && !earlyDetonate)
        {
            newFireball.transform.position = Vector3.Lerp(start, end, elapsedTime / duration);
            newFireball.transform.LookAt(cam.transform);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        earlyDetonate = false;
        Vector3 yTransform = new(0f, 2f, 0f);
        newFireball.transform.position -= yTransform;
        animator.SetTrigger("Detonate");
        yield return new WaitForSeconds(1);
        elapsedTime = 0f;
        Destroy(newFireball);
        yield break;
    }

    public void SetEarlyDetonate(bool val)
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
