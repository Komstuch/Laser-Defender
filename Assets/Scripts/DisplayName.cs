using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayName : MonoBehaviour
{
    Text displayText;
    string playerName;

    void Start()
    {
        playerName = PlayerPrefs.GetString("PlayerName", "Player");
        Debug.Log("Name form playerprefs: " + playerName);
        displayText = GetComponent<Text>();
        displayText.text = playerName;
    }
}
