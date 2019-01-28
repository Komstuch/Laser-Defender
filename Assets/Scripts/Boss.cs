using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Enemy bossScript;
    private HealthBar healthBar;

    private float maxHP;

    void Start()
    {
        bossScript = GetComponent<Enemy>();
        healthBar = FindObjectOfType<HealthBar>();
        maxHP = bossScript.GetHP();
        healthBar.SetMaxHP(maxHP);
        UpdateHealthbar(maxHP);
    }

    public void UpdateHealthbar(float currentHP)
    {
       healthBar.UpdateBar(currentHP);
    }
}
