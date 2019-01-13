using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    [SerializeField] InputField nameInput;

    string playerName;
    public void SetPlayerName()
    {
        playerName = nameInput.text.ToString();
        Debug.Log("name form input " + playerName);
        PlayerPrefs.SetString("PlayerName", playerName);
    }
}
