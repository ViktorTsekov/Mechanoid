using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject gameMode;
    public GameObject settings;
    public GameObject instructions;
    public GameObject credits;
    public GameObject settingsMessage;

    public Slider sfxSlider;
    public Slider soundtrackSlider;

    private GameObject audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("SoundManager");

        if (sfxSlider != null && soundtrackSlider != null)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1f);
            soundtrackSlider.value = PlayerPrefs.GetFloat("soundtrackVolume", 1f);
        }
    }

    public void showPanel(string panelName)
    {
        mainMenu.SetActive(false);

        switch (panelName)
        {
            case "gameMode": gameMode.SetActive(true); break;
            case "settings": settings.SetActive(true); break;
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
        audioManager.GetComponent<SoundManager>().updateTracks();
        settingsMessage.SetActive(true);
    }

    public void quit()
    {
        Application.Quit();
    }
}