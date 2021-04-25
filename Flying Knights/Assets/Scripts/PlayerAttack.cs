﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

    public class PlayerAttack : MonoBehaviour
    {
	/* Attacher ce script sur le player et créer un empty avec un boxcollider et un tag "PlayerAttack"
	   Mettre cet empty en enfant du joueur et ensuite le drag&drop dans la var attackCollider du script*/
        [SerializeField] private SFXPlayerAttack attackCollider;
        [SerializeField] private bool isAttacking;
        [SerializeField] private float attackCooldownTime = 0.8f;

        [Tooltip("The animator for the player model")]
        [SerializeField] private Animator playerAnimator;
        private void FixedUpdate()
        {
            
            if (isAttacking)
            {
                if(!attackCollider.isActive())
                {
                    playerAnimator.SetTrigger("AttackTrigger");
                    isAttacking = false;
                }
                else
                {
                    isAttacking = false;
                }
            }
        }

        public void attackBoxEventEnable()
        {
            attackCollider.SetActive(true);
        }
        public void attackBoxEventDisable()
        {
            attackCollider.SetActive(false);
            playerAnimator.ResetTrigger("AttackTrigger");
        }


        public void OnAttack()
        {
            isAttacking = true;
        } 
    }