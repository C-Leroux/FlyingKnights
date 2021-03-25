using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

    public class PlayerAttack : MonoBehaviour
    {
	/* Attacher ce script sur le player et créer un empty avec un boxcollider et un tag "PlayerAttack"
	   Mettre cet empty en enfant du joueur et ensuite le drag&drop dans la var attackCollider du script*/
        [SerializeField] private GameObject attackCollider;
        [SerializeField] private bool isAttacking;
        [SerializeField] private float attackCooldownTime = 0.8f;

        [Tooltip("The animator for the player model")]
        [SerializeField] private Animator playerAnimator;

        private void FixedUpdate()
        {
            
            if (isAttacking)
            {
                if(!attackCollider.activeSelf)
                {
                    playerAnimator.SetTrigger("AttackTrigger");
                    attackCollider.SetActive(true);
                    Invoke("DisableAttack", attackCooldownTime);
                    isAttacking = false;
                }
                else
                {
                    isAttacking = false;
                }
            }
        }

        private void DisableAttack()
        {
            attackCollider.SetActive(false);
            playerAnimator.ResetTrigger("AttackTrigger");
        }

        public void OnAttack()
        {
            isAttacking = true;
        } 
    }