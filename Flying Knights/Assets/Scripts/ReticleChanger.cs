using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ReticleChanger : MonoBehaviour
{

    [SerializeField] private Camera camera;
    public SpriteRenderer firstRenderer;
    public SpriteRenderer secondRenderer;
    public SpriteRenderer thirdRenderer;

    public Sprite untargettingSprite;
    public Sprite targettingSprite;


    public float maxRange = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, maxRange))
        {
            firstRenderer.sprite = targettingSprite;
            secondRenderer.sprite = targettingSprite;
            thirdRenderer.sprite = targettingSprite;
        }
        else
        {
            firstRenderer.sprite = untargettingSprite;
            secondRenderer.sprite = untargettingSprite;
            thirdRenderer.sprite = untargettingSprite;
        }
    }
}
