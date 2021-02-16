using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
    public  bool Paused=false;
    public GameObject MenuPause;
    void Start()
    {
        Paused = false;

    }
    public void ActivatePause()
    {
        MenuPause.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }
    public void Resume()
    {
        MenuPause.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    public bool getPaused()
    {
        return this.Paused;
    }
}
