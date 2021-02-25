using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    
    public  bool Paused=false;
    public GameObject MenuPause;
    public GameObject Settings;
    void Start()
    {
        Paused = false;

    }
    public void ActivatePause()
    {
        
        MenuPause.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        Paused = true;
    }
    public void Resume()
    {
        MenuPause.SetActive(false);
        Settings.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        Paused = false;
    }

    public bool getPaused()
    {
        return this.Paused;
    }

    public void SettingsMenu()
    {

        MenuPause.SetActive(false);
        Settings.SetActive(true);

    }

    public void Menu()
    {
        Debug.Log("Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Retry()
    {
        Debug.Log("Retry");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Preload");
    }
}
