using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public GameObject Menu;
    public GameObject LevelMenu;
    public GameObject Settings;
    void Start()
    {


    }
    public void Level1()
    {
        SceneManager.LoadScene("Preload");
  

    }
    public void Back()
    {
        LevelMenu.SetActive(false);
        Menu.SetActive(true);

    }
}
