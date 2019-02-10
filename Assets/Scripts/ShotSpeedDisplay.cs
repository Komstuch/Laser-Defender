using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotSpeedDisplay : MonoBehaviour
{
    PlayerController player;
    Text speedText;
    float baseShotSpeed;

    void Start()
    {
        speedText = GetComponent<Text>();
        player = FindObjectOfType<PlayerController>();
        baseShotSpeed = 1f / player.GetFiringRate();
    }

    void Update()
    {
        float currentShotSpeed = 1f / player.GetFiringRate();
        float percentage = currentShotSpeed / baseShotSpeed * 100f;
        speedText.text = ((int)percentage).ToString() + "%";
    }
}
