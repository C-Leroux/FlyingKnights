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
    int c = 0;
    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            StartCoroutine(UseSFX(woodSFX));
            c += 1;
        }
        else if (other.CompareTag("ColossusPart") || other.CompareTag("Colossus"))
        {
            StartCoroutine(UseSFX(bloodSfx));
            c += 2;
        }
        else if (other.CompareTag("WeakPoint")) c += 4; ;
        
        if(c == 3)
        {
            audioSource.clip = failClip;
            audioSource.Play();
        }
        else if ( c == 2)
        {
            audioSource.clip = failClip;
            audioSource.Play();
        }
        else if ( c == 1)
        {
            audioSource.clip = woodClip;
            audioSource.Play();
        }
    }

    private IEnumerator UseSFX(GameObject sfx)
    {
        sfx.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        sfx.SetActive(false);
    }

    public void SetActive(bool b)
    {
        this.gameObject.SetActive(b);
        c = 0;
    }

    public bool isActive()
    {
        return gameObject.activeSelf;
    }
}
