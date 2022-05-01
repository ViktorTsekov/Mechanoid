using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject[] controlButtons;

    public GameObject mainMenu;
    public GameObject gameMode;
    public GameObject settings;
    public GameObject instructions;
    public GameObject credits;
    public GameObject settingsMessage;
    public GameObject anyKey;

    public Slider sfxSlider;
    public Slider soundtrackSlider;

    private GameObject audioManager;
    private GameObject controlsManager;
    private GameObject inputButton;

    private bool waitForInput = false;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        audioManager = GameObject.FindGameObjectWithTag("SoundManager");
        controlsManager = GameObject.FindGameObjectWithTag("ControlsManager");

        setValuesOfKeyButtons();

        if (sfxSlider != null && soundtrackSlider != null)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1f);
            soundtrackSlider.value = PlayerPrefs.GetFloat("soundtrackVolume", 1f);
        }
    }

    void Update()
    {
        if(waitForInput)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    inputButton.transform.GetChild(0).GetComponent<Text>().text = "" + kcode;
                    waitForInput = false;
                    anyKey.SetActive(false);
                }
            }
        }
    }

    public void showPanel(string panelName)
    {
        mainMenu.SetActive(false);

        switch (panelName)
        {
            case "gameMode": gameMode.SetActive(true); break;
            case "settings": settings.SetActive(true); setValuesOfKeyButtons(); setValueOfSliders();  break;
            case "instructions": instructions.SetActive(true); break;
            case "credits": credits.SetActive(true); break;
        }
    }

    public void goBack()
    {
        mainMenu.SetActive(true);
        gameMode.SetActive(false);
        settings.SetActive(false);
        instructions.SetActive(false);
        credits.SetActive(false);
        settingsMessage.SetActive(false);
    }

    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void save()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("soundtrackVolume", soundtrackSlider.value);

        foreach (GameObject button in controlButtons)
        {
            PlayerPrefs.SetString(button.name, button.transform.GetChild(0).GetComponent<Text>().text);
        }

        audioManager.GetComponent<SoundManager>().updateTracks();
        settingsMessage.SetActive(true);
    }

    public void changeControl(GameObject buttonInstance)
    {
        inputButton = buttonInstance;
        waitForInput = true;
        anyKey.SetActive(true);
    }

    public void quit()
    {
        Application.Quit();
    }

    private void setValuesOfKeyButtons()
    {
        foreach (GameObject button in controlButtons)
        {
            button.transform.GetChild(0).GetComponent<Text>().text = "" + controlsManager.GetComponent<ControlsManager>().getKey(button.name);
        }
    }

    private void setValueOfSliders()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1f);
        soundtrackSlider.value = PlayerPrefs.GetFloat("soundtrackVolume", 1f);
    }
}