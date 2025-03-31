using UnityEngine;
using System.Collections;
using static UnityEngine.Rendering.DebugUI.Table;
using Unity.VisualScripting;

public class SwordScript : MonoBehaviour
{
    private Animator animator;

    public PlayerController pc;
    public GameObject sword;
    public SkinnedMeshRenderer swordRenderer;
    public GameObject swordTemp;
    public Camera cam;
    public CharacterController character;
    public CapsuleCollider capColl;

    private bool swinging = false;
    private bool terminate = false;
    private bool castable;

    public bool GetCastable() {  return castable; }

    public IEnumerator Swing()
    {
        if (swinging)
        {
            yield break;
        }
        swinging = true;
        animator.SetTrigger("Swing");
        Vector3 v3 = new(-1.6f, 0f, 0.3f);
        capColl.center += v3;
        yield return new WaitForSeconds(1f);
        capColl.center -= v3;
        swinging = false;
        yield break;
    }

    public IEnumerator SwordStart()
    {
        castable = true;
        swordRenderer.enabled = true;
        animator.SetTrigger("SwordActive");

        /*
        Vector3 pos = pc.transform.position;
        Quaternion rot = sword.transform.rotation;

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
        */

        while (!terminate)
        {
            yield return null;
        }
        pc.StartCD();
        terminate = false;
        swordRenderer.enabled = false;
        animator.SetTrigger("SwordInactive");
        castable = false;
        yield break;
    }

    public void SetTerminate(bool term)
    {
        terminate = term;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
