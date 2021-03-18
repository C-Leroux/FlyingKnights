﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armIK : MonoBehaviour
{
    

    [Tooltip("The end of the grappling hook, for the IK")]
    [SerializeField] private GameObject HookObject;

    [Tooltip("The owner of the hookshoot script")]
    [SerializeField] private Hookshot hookShootScript;

    [Tooltip("The speed at which the arm returns to the normal animation after IK")]
    [SerializeField] private float armReturnSpeed = 0.2f;

    private Animator playerAnimator;

    private float weight = 0f;


    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    
    void OnAnimatorIK(int layerIndex)
    {
        //setting the IK
        if(hookShootScript.GetisHooked())
        {
            weight = 1f;
        }
        else
        {
            if(weight>0)
            {
                weight -= armReturnSpeed*Time.deltaTime;
                if(weight < 0) weight = 0;
            }
        }

        
            playerAnimator.SetIKPosition(AvatarIKGoal.RightHand,HookObject.transform.position);
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand,weight);
    }
}