using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ReticleChanger : MonoBehaviour
{

    [SerializeField] private Camera localCamera;
    [SerializeField] private Hookshot hook = null;
    public SpriteRenderer firstRenderer;
    public SpriteRenderer secondRenderer;
    public SpriteRenderer thirdRenderer;

    public Sprite untargettingSprite;
    public Sprite targettingSprite;

    //public GameObject DebugObject;

    public float maxRange;
    private RaycastHit raycastHit;
    private bool raycast;
    // Start is called before the first frame update
    void Start()
    {
        maxRange = hook.maxDist;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Raycast(localCamera.transform.position, localCamera.transform.forward, out raycastHit, maxRange))
        {
            raycast = true;
            firstRenderer.sprite = targettingSprite;
            secondRenderer.sprite = targettingSprite;
            thirdRenderer.sprite = targettingSprite;
        }
        else
        {
            raycast = false;
            firstRenderer.sprite = untargettingSprite;
            secondRenderer.sprite = untargettingSprite;
            thirdRenderer.sprite = untargettingSprite;
        }
    }

    public Vector3 GetRaycastHit()
    {
        if (raycast)
        {
            return raycastHit.point;
        }
        else
        {
            //Instantiate(DebugObject, localCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, maxRange)), new Quaternion(0,0,0,0));
            return localCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, maxRange));
        }
    }
}
