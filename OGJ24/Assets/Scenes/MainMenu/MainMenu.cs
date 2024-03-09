using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public static float soundVolume;
    public static float musicVolume;
    [SerializeField] private Slider volumeMusicSlider;
    [SerializeField] private AudioSource soundTest;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
        }
        /*if (!PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetFloat("soundVolume", 0.5f);
        }*/
        Load();
    }

    public void ChangeMusicVolume()
    {
        AudioListener.volume = volumeMusicSlider.value;
        Save();
    }

    /*public void ChangeSoundVolume()
    {
       AudioSource.PlayClipAtPoint(soundTest.clip, volumeSoundSlider.transform.position, volumeSoundSlider.value);
       Save();
    }*/

    private void Load()
    {
        volumeMusicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        musicVolume = volumeMusicSlider.value;
        /*volumeSoundSlider.value = PlayerPrefs.GetFloat("soundVolume");
        soundVolume = volumeSoundSlider.value;*/
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeMusicSlider.value);
        musicVolume = volumeMusicSlider.value;
        /*PlayerPrefs.SetFloat("soundVolume", volumeSoundSlider.value);
        soundVolume = volumeSoundSlider.value;*/
    }

    public void Play()
    {
        SceneManager.LoadScene("test");
    }

    public void Quit()
    {
        Application.Quit(0);
    }

    /*public void ChangeMusicVolume(float newValue)
    {
        musicVolume = newValue;
    }
    
    public void ChangeSoundVolume(float newValue)
    {
        soundVolume = newValue;
    }*/
}
