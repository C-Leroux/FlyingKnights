﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public GameObject Menu;
    public GameObject SettingMenu;
    public AudioMixer AudioMixer;

    public void SetMusicVolume(float volume)
    {
        AudioMixer.SetFloat("MusicVolume", volume * 100 - 80);
    }
    public void SetEffectVolume(float volume)
    {
        AudioMixer.SetFloat("EffectVolume", volume * 100 - 80);
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
