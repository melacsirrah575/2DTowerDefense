using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    //Reference to the PauseMenu gameobject
    public GameObject m_pauseMenu;

    //Reference to the SettingsMenu gameobject
    public GameObject m_settingsMenu;
    public GameObject m_mainMenu;

    [HideInInspector]
    public bool m_pauseMenuIsActive = false;
    [HideInInspector]
    public bool m_settingsMenuIsActive = false;
    [HideInInspector]
    public bool m_mainMenuIsActive = true;

    public void SwitchToSettingsMenu()
    {
        if(m_mainMenuIsActive)
        {
            m_pauseMenu.SetActive(false);
            m_pauseMenuIsActive = false;
            m_settingsMenu.SetActive(true);
            m_settingsMenuIsActive = true;
            m_mainMenu.SetActive(false);
            m_mainMenuIsActive = true;
        } else
        {
            m_pauseMenu.SetActive(false);
            m_pauseMenuIsActive = false;
            m_settingsMenu.SetActive(true);
            m_settingsMenuIsActive = true;
            m_mainMenu.SetActive(false);
            m_mainMenuIsActive = false;
        }
    }

    public void SwitchToPauseMenu()
    {
        if (m_mainMenuIsActive && m_settingsMenuIsActive)
        {
            m_pauseMenu.SetActive(false);
            m_pauseMenuIsActive = false;
            m_settingsMenu.SetActive(false);
            m_settingsMenuIsActive = false;
            m_mainMenu.SetActive(true);
            m_mainMenuIsActive = true;
        } else
        {
            m_pauseMenu.SetActive(true);
            m_pauseMenuIsActive = true;
            m_settingsMenu.SetActive(false);
            m_settingsMenuIsActive = false;
            m_mainMenu.SetActive(false);
            m_mainMenuIsActive = false;
        }
    }

    public void startGame()
    {
        m_mainMenuIsActive = false;
    }
}
