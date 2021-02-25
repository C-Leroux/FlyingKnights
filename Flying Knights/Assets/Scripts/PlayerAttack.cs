using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

    public class PlayerAttack : MonoBehaviour
    {
	/* Attacher ce script sur le player et créer un empty avec un boxcollider et un tag "PlayerAttack"
	   Mettre cet empty en enfant du joueur et ensuite le drag&drop dans la var collAttack du script*/


        [SerializeField] private GameObject collAttack;

        [SerializeField] private bool isAttacking;

        private void FixedUpdate()
        {
            
            if (isAttacking)
            {
                collAttack.SetActive(true);
                Invoke("DisabAttack", 0.5f);
                isAttacking = false;
            }
        }

        private void DisabAttack()
        {
            collAttack.SetActive(false);
        }

        public void OnAttack()
        {
            isAttacking = true;
        } 
    }