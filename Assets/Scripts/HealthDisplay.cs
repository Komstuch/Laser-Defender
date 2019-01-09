using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Text healthText;
    int health;
    
    void Start()
    {
        healthText = GetComponent<Text>();
        health = FindObjectOfType<PlayerController>().GetHealth();
        healthText.text = health.ToString();
    }
    
    public void SetHealth(int health)
    {
        healthText.text = health.ToString();
    }
}
