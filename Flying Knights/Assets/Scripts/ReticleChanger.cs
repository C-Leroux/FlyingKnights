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
    public LayerMask IgnoreInRaycast;

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
        if (Physics.Raycast(localCamera.transform.position, localCamera.transform.forward, out raycastHit, maxRange,~IgnoreInRaycast))
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
            if(Physics.Raycast(localCamera.transform.position, localCamera.transform.forward, out raycastHit, maxRange,~IgnoreInRaycast))
            {
                return raycastHit.point;
            }
            else
            {
                return localCamera.transform.position + 100*(localCamera.transform.forward);
            }
        }
    }
}
