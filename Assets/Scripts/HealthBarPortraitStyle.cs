using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPortraitStyle : MonoBehaviour
{
    public Image linearBar;
    public Image radialBar;
    public int maxHealth = 1000;
    private int _health;

    void Start()
    {
        _health = maxHealth;
    }
    public void AddHealth(int value)
    {
        _health += value;
        if (_health > maxHealth)
        {
            _health = maxHealth;
        }
        UpdateHealthBar();
    }

    public bool RemoveHealth(int value)
    {
        _health -= value;
        if (_health <= 0)
        {
            _health = 0;
            UpdateHealthBar();
            return true;
        }
        UpdateHealthBar();
        return false;
    }

    private void UpdateHealthBar() {
        float ratio = _health * 1f / maxHealth;
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
