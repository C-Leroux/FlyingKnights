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
        Debug.Log("AH");
        SceneManager.LoadScene("Level2");
  

    }
    public void Back()
    {
        LevelMenu.SetActive(false);
        Menu.SetActive(true);

    }
}
