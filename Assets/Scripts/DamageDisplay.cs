using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour
{
    PlayerController player;
    Text damageText;
    float baseDamage;

    void Start()
    {
        damageText = GetComponent<Text>();
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        damageText.text = player.GetProjectileDamage().ToString();
    }
}
