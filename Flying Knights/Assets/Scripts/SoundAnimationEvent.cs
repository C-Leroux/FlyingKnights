using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAnimationEvent : MonoBehaviour
{
    AudioSource sourceAudio;
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
        sourceAudio = Sources[1];
        sourceAudio.Play();
    }
    public void PlaySlash()
    {
        sourceAudio = Sources[0];
        sourceAudio.Play();
    }

    public void PlayJumpSound()
    {
        sourceAudio = Sources[2];
        sourceAudio.Play();
    }

    //Sources du colosse possede 2 source : 0 - Bruit de pas
    //                                      1 - Cri de mort
    //                                      2 - Coup de Poing
    //                                      3 - Cri d'attaque
    public void PlayStepColosse()
    {
        sourceAudio = Sources[0];
        sourceAudio.Play();
    }

    public void PlayDieColosse()
    {
        sourceAudio = Sources[1];
        sourceAudio.Play();
    }

    public void PlayAttackColosse()
    {
        sourceAudio = Sources[2];
        sourceAudio.Play();
    }
    
    public void PlayAttackGrowlColosse()
    {
        sourceAudio = Sources[3];
        sourceAudio.Play();
    }
}
