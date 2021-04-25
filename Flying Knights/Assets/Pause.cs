using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    
    public  bool Paused=false;
    public GameObject MenuPause;
    public GameObject Settings;
    public GameObject Controls;
    
    void Start()
    {
        Paused = false;
        

    }
    public void ActivatePause()
    {
        
        MenuPause.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        Paused = true;
    }
    public void Resume()
    {
        MenuPause.SetActive(false);
        Settings.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void ControlMenu()
    {
        MenuPause.SetActive(false);
        Controls.SetActive(true);
    }
}
