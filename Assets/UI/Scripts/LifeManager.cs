using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    public int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void LoseHealth()
    {
        currentHealth--;

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
