using UnityEngine;
using System.Collections;
using static UnityEngine.Rendering.DebugUI.Table;
using Unity.VisualScripting;

public class SwordScript : MonoBehaviour
{
    public PlayerController pc;
    public GameObject sword;
    public GameObject swordTemp;
    private GameObject currSword;
    public Camera cam;
    public CharacterController character;
    private float elapsedTime = 0f;
    private float swingTime = 0f;
    private float swingDuration = 1f;
    private float duration = 15f;
    private bool earlyTerminate = false;
    private bool castable;

    public bool getCastable() {  return castable; }

    public IEnumerator Swing()
    {
        if (swingTime != 0)
        {
            yield break;
        }
        Vector3 start = currSword.transform.localPosition;
        Vector3 end = new Vector3(start.x - 2f, start.y, start.z);
        while (swingTime < swingDuration)
        {
            currSword.transform.localPosition = Vector3.Lerp(start, end, swingTime / swingDuration);
            swingTime += Time.deltaTime;
            yield return null;
        }
        currSword.transform.localPosition = end;
        swingTime = 0f;
        while (swingTime < 0.5f)
        {
            swingTime += Time.deltaTime;
            yield return null;
        }
        swingTime = 0f;
        currSword.transform.localPosition = start;
        yield break;
    }

    public IEnumerator SwordTimer()
    {
        castable = true;
        Vector3 pos = pc.transform.position;
        Quaternion rot = pc.transform.rotation;

        GameObject newSwordTemp = Instantiate(swordTemp, pos, rot);
        newSwordTemp.transform.SetParent(character.transform);

        Vector3 offset = cam.transform.localPosition;
        float x = offset.x + 1;
        float y = character.transform.position.y + 0.5f;
        float z = 3f;
        Vector3 final = new Vector3(x, y, z);
        newSwordTemp.transform.localPosition = final;

        Vector3 world = newSwordTemp.transform.TransformPoint(final);
        currSword = Instantiate(sword, world, rot);

        currSword.transform.SetParent(character.transform);
        currSword.transform.localPosition = final;

        while (elapsedTime < duration && !earlyTerminate)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(currSword);
        castable = false;
        elapsedTime = 0f;
        yield break;
    }

    public void setEarlyTerminate(bool term)
    {
        earlyTerminate = term;
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
