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
        GetName();
    }

    public void GetName()
    {
        playerName = PlayerPrefsManager.GetPlayerName();
        displayText = GetComponent<Text>();
        displayText.text = playerName;
    }
}
