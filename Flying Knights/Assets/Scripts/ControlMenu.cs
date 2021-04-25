using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControlMenu : MonoBehaviour
{
    [SerializeField] private GameObject controlMenu;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject tKeyboardMouse;
    [SerializeField] private GameObject tGrappling;
    [SerializeField] private GameObject tEnemies;

    [SerializeField] private GameObject previous;
    [SerializeField] private GameObject next;

    private int currentImage = 0;

    // Update is called once per frame
    void Update()
    {
        previous.SetActive(currentImage != 0);

        next.SetActive(currentImage != 2);
    }

    public void GoBack()
    {
        controlMenu.SetActive(false);
        menu.SetActive(true);
    }

    public void Next()
    {
        switch (currentImage)
        {
            case 0:
                tKeyboardMouse.SetActive(false);
                tGrappling.SetActive(true);
                currentImage = 1;
                break;
            case 1:
                tGrappling.SetActive(false);
                tEnemies.SetActive(true);
                currentImage = 2;
                break;
            case 2: 
                break;
        }
    }

    public void Previous()
    {
        switch (currentImage)
        {
            case 0:
                break;
            case 1:
                tGrappling.SetActive(false);
                tKeyboardMouse.SetActive(true);
                currentImage = 0;
                break;
            case 2:
                tEnemies.SetActive(false);
                tGrappling.SetActive(true);
                currentImage = 1;
                break;
        }
    }

}
