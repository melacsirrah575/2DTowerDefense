using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Creating a Singleton aka public static instance of GameManager initialized to 'null' to ensure it's empty to start
    public static GameManager Instance = null;

    //Public reference of the PauseMenu object to allow access to turn the gameobject on and off
    public GameObject m_pauseMenu;
    public GameObject m_mainMenu;

    //public bool to track if game is in a paused state or not
    public bool m_isPaused = false;

    MenuManager menuManager;

    private void Awake()
    {
        //On Awake, create the instance of the GameManager that will be uised for the duration of the program
        SetInstance();
        MainMenu();
    }

    private void Start()
    {
        menuManager = GetComponent<MenuManager>();
    }

    private void SetInstance()
    {
        //Check if this script/object is being used as the Instance of the GameManager
        //if not, destroy that imposter and use this script/object instead
        if(Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
        //Don't destroy this gameObject is another scene is loaded
        //will seperate this gameObject into it's own instance that exists below the active scene in unity hierarchy
        DontDestroyOnLoad(gameObject);
    }

    //Function to pause the game and activate the PauseMenu object
    public void PauseGame()
    {
        //Check to make sure the bool is false, indicating that the game is NOT paused at the moment...
        if(!m_isPaused)
        {
            //Set the timescale of the game to 0. This will disable actions/movement based on rigidbodies
            Time.timeScale = 0;

            //Set the m_isPaused bool true, indicating a paused gamestate
            m_isPaused = true;

            //Turn on the PauseMenu gameobject
            m_pauseMenu.SetActive(true);
        }
    }
    //Function to unpause the game and deactivate the PauseMenu object
    public void ResumeGame()
    {
        //Check to make sure the bool is true, indicating that the game IS paused at the moment...
        if(m_isPaused)
        {
            //Set the timescale of the game to 1, this will enable actions/movement based on rigidbodies
            Time.timeScale = 1;

            //Set the m_isPaused bool false, indicating a unpaused gamestate
            m_isPaused = false;

            //Turn off the PauseMenu and MainMenu gameobject
            if(menuManager.m_mainMenuIsActive)
            {
                m_pauseMenu.SetActive(false);
                m_mainMenu.SetActive(true);
                menuManager.m_settingsMenuIsActive = false;
                menuManager.m_settingsMenu.SetActive(false);
                m_isPaused = true;
            } else
            {
                m_pauseMenu.SetActive(false);
                m_mainMenu.SetActive(false);
                menuManager.m_settingsMenuIsActive = false;
                menuManager.m_settingsMenu.SetActive(false);
            }
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 0;
        m_isPaused = true;
        m_mainMenu.SetActive(true);

    }
}
