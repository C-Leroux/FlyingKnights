using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject Menu;
    public GameObject SettingMenu;
    void Start()
    {


    }

    public void Back()
    {
        SettingMenu.SetActive(false);
        Menu.SetActive(true);
    }
}
