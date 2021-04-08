using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject woodSFX;
    [SerializeField] private GameObject bloodSfx;
    [SerializeField] private AudioClip woodClip;
    [SerializeField] private AudioClip failClip;
    
    private AudioSource audioSource;

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            StartCoroutine(UseSFX(woodSFX));
            audioSource.clip = woodClip;
            audioSource.Play();
        }
        else if ( other.CompareTag("ColossusPart") ||other.CompareTag("Colossus"))
        {
            StartCoroutine(UseSFX(bloodSfx));
            audioSource.clip = failClip;
            audioSource.Play();
        }
    }

    private IEnumerator UseSFX(GameObject sfx)
    {
        sfx.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        sfx.SetActive(false);
    }
}
