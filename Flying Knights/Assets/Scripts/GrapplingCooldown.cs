﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingCooldown : MonoBehaviour
{
    //References
    public float grapplingCooldown;
    public float inactiveTime = 0f;
    public bool isReady = true;

    //Part of recticle material
    public SpriteRenderer firstRenderer;
    public SpriteRenderer secondRenderer;
    public SpriteRenderer thirdRenderer;

    //Basic Color
    private UnityEngine.Color readyColor;
    private UnityEngine.Color cooldownColor;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate basic colors
        readyColor = new UnityEngine.Color(0f, 159f/255f, 255f/255f, 255f/255f);
        cooldownColor = new UnityEngine.Color(0f, 50f/255f, 80f/255f, 255f/255f);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isReady)
        {
            Debug.Log("Enter !isReady boucle");
            //All reticle became in cooldownColor
            firstRenderer.color = cooldownColor;
            secondRenderer.color = cooldownColor;
            thirdRenderer.color = cooldownColor;

            //Initialize and start the cooldown
            inactiveTime += Time.deltaTime;
            Debug.Log("inactive time  : " + inactiveTime);
            //first third of cooldown
            if(inactiveTime > grapplingCooldown / 3f)
            {
                Debug.Log("firstThird is passed");
                firstRenderer.color = readyColor;
            }

            //second third of cooldown
            if(inactiveTime > 2 * grapplingCooldown / 3f)
            {
                Debug.Log("secondThird is passed");
                secondRenderer.color = readyColor;
            }

            //cooldown is passed
            if(inactiveTime > grapplingCooldown)
            {
                Debug.Log("thirdThird is passed");
                thirdRenderer.color = readyColor;
                inactiveTime = 0f;
                isReady = true;
            }
            
        }
    }
}