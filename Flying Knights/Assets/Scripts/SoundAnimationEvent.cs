using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAnimationEvent : MonoBehaviour
{
    AudioSource audio;
    [SerializeField] AudioSource[] Sources;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Sources du player possede 2 sources : 0 - Slash d'epee
    //                                      1 - Bruit de pas  
    //                                      2 - Saut
    public void PlayStepPlayer()
    {
        audio = Sources[1];
        audio.Play();
    }
    public void PlaySlash()
    {
        audio = Sources[0];
        audio.Play();
    }

    public void PlayJumpSound()
    {
        audio = Sources[2];
        audio.Play();
    }

    //Sources du colosse possede 2 source : 0 - Bruit de pas
    //                                      1 - Cri de mort
    public void PlayStepColosse()
    {
        audio = Sources[0];
        audio.Play();
    }

    public void PlayDieColosse()
    {
        audio = Sources[1];
        audio.Play();
    }

    
}
