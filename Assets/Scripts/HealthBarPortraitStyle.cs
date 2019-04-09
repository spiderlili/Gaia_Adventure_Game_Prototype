using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPortraitStyle : MonoBehaviour
{
    public Image linearBar;
    public Image radialBar;
    public int maxHealth = 1000;
    public int health;

    void Start()
    {
        health = maxHealth;
    }
    public void AddHealth(int value)
    {
        health += value;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        UpdateHealthBar();
    }

    public bool RemoveHealth(int value)
    {
        health -= value;
        if (health <= 0)
        {
            health = 0;
            UpdateHealthBar();
            return true;
        }
        UpdateHealthBar();
        return false;
    }

    public void RestoreFullHealth() {
        health = maxHealth;
        linearBar.fillAmount = 1.0f;
        radialBar.fillAmount = 0.75f;
    }
    
    private void UpdateHealthBar() {
        float ratio = health * 1f / maxHealth;
        Debug.Log(ratio);
        if (ratio > 0.6)
        {
            linearBar.fillAmount = (ratio - 0.6f) * 2.5f;
            radialBar.fillAmount = 0.75f;
        }
        else
        { linearBar.fillAmount = 0;
            radialBar.fillAmount = 0.75f * ratio * 10f / 6f;
        }
    }

}
