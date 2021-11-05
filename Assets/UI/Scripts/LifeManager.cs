using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LifeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayHealth;
    [SerializeField] int maxHealth = 10;
    public int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }

    void Awake()
    {
        currentHealth = maxHealth;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        displayHealth.text = "Lives: " + currentHealth;
    }

    public void LoseHealth()
    {
        currentHealth--;
        UpdateDisplay();

        if (currentHealth <= 0)
        {
            //Future Update will bring player back to menu instead
            Application.Quit();
        }
    }

    //Not in use yet but can be for future
    public void GainHealth()
    {
        currentHealth++;
    }
}
