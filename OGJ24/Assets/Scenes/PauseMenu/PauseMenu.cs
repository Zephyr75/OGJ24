using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject pauseMenu;
    private PlayerInputs inputs;
    private InputAction echapAction;
    [SerializeField] private Slider volumeMusicSlider;

    private bool isPaused;
    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Load();
    }

    private void Awake()
    {
        inputs = new PlayerInputs();
    }
    
    private void OnEnable()
    {
        echapAction = inputs.Player.Echap;
        echapAction.Enable();
        /*lookAction = inputs.Player.Look;
        lookAction.Enable();*/
    }
    
    private void OnDisable()
    {
        echapAction.Disable();
        //lookAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (echapAction.WasPressedThisFrame())
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void ChangeMusicVolume()
    {
        AudioListener.volume = volumeMusicSlider.value;
        Save();
    }
    
    private void Load()
    {
        volumeMusicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        MainMenu.musicVolume = volumeMusicSlider.value;
        /*volumeSoundSlider.value = PlayerPrefs.GetFloat("soundVolume");
        soundVolume = volumeSoundSlider.value;*/
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeMusicSlider.value);
        MainMenu.musicVolume = volumeMusicSlider.value;
        /*PlayerPrefs.SetFloat("soundVolume", volumeSoundSlider.value);
        soundVolume = volumeSoundSlider.value;*/
    }
}
