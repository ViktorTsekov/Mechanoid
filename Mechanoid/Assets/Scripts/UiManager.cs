using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject gameMode;
    public GameObject controls;
    public GameObject instructions;
    public GameObject credits;

    public void showPanel(string panelName)
    {
        mainMenu.SetActive(false);

        switch (panelName)
        {
            case "gameMode": gameMode.SetActive(true); break;
            case "controls": controls.SetActive(true); break;
            case "instructions": instructions.SetActive(true); break;
            case "credits": credits.SetActive(true); break;
        }
    }

    public void goBack()
    {
        mainMenu.SetActive(true);
        gameMode.SetActive(false);
        controls.SetActive(false);
        instructions.SetActive(false);
        credits.SetActive(false);
    }

    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void quit()
    {
        Application.Quit();
    }
}