using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public GameObject Menu;
    public GameObject SettingMenu;
    public AudioMixer AudioMixer;

    public void SetVolume(float Volume)
    {
        AudioMixer.SetFloat("volume", Volume*100-80);
    }

    public void setQuality (int quality)
    {
        Debug.Log(quality);
        QualitySettings.SetQualityLevel(quality);
    }
    public void Back()
    {
        SettingMenu.SetActive(false);
        Menu.SetActive(true);

    }
}
