using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    [SerializeField] GameObject nameInputField;
    [SerializeField] GameObject introText;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject nameText;

    LevelManager levelManager;
    InputField nameInput;
    string playerName;

    private void Start()
    {
        nameInput = nameInputField.GetComponent<InputField>();
        levelManager = FindObjectOfType<LevelManager>();

        nameInputField.SetActive(false);
        startButton.SetActive(false);
        nameText.SetActive(false);

        playerName = PlayerPrefs.GetString("PlayerName");

        if (playerName == "")
        {
            nameInputField.SetActive(true);
            startButton.SetActive(true);

            introText.GetComponent<Text>().text = "What is your name ?";
        } else
        {
            nameText.SetActive(true);
            introText.GetComponent<Text>().text = "Welcome back";
            StartCoroutine(levelManager.WaitAndLoad("Start Menu", 3f));
        }

    }

    public void SetPlayerName()
    {
        playerName = nameInput.text.ToString();
        PlayerPrefs.SetString("PlayerName", playerName);
    }
}
