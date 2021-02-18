using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Levels;
    public GameObject Settings;
    void Start()
    {
        

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
}
