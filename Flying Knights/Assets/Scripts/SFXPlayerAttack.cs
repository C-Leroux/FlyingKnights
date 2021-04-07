using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject woodSFX;
    [SerializeField] private GameObject bloodSfx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            StartCoroutine(UseSFX(woodSFX));
        }
        
        else if (other.CompareTag("Colossus"))
        {
            StartCoroutine(UseSFX(bloodSfx));
        }
    }

    private IEnumerator UseSFX(GameObject sfx)
    {
        sfx.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        sfx.SetActive(false);
    }
}
