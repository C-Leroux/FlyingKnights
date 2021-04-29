using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Levels;
    public GameObject Settings;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void LevelSelection()
    {

        Menu.SetActive(false);
        Levels.SetActive(true);

    }
   
    public void SettingsMenu()
    {

        Menu.SetActive(false);
        Settings.SetActive(true);

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {

        SceneManager.LoadScene("Level8");

    }
}
