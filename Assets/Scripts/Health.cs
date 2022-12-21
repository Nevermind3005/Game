using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{

    private int initialHealth;
    private int currentHealth;

    public Health(int initialHealth)
    {
        this.initialHealth = initialHealth;
        this.currentHealth = initialHealth;
    }

    public int InitialHealth
    {
        get => initialHealth;
        private set => this.initialHealth = value;
    }

    public int CurrentHealth
    {
        get => currentHealth;
        private set => this.currentHealth = value;
    }

    public void Demage(int demageValue)
    {
        if (currentHealth - demageValue > 0)
        {
            currentHealth -= demageValue;
        }
        else
        {
            currentHealth = 0;
        }
    }

    public void Kill()
    {
        currentHealth = 0;
    }
    
}
