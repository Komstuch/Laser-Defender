using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameChanger : MonoBehaviour
{
    [SerializeField] GameObject changeNameButton;
    [SerializeField] GameObject quitButton;
    [SerializeField] GameObject inputMenu;
    [SerializeField] GameObject nameDisplay;

    Authenticator authenticator;
    string newName;

    private void Start()
    {
        authenticator = FindObjectOfType<Authenticator>();
    }

    public void ChangeName()
        {
            changeNameButton.SetActive(false);
            quitButton.SetActive(false);
            inputMenu.SetActive(true);
        }

    public void ConfirmChange()
    {
        newName = inputMenu.GetComponent<InputField>().text.ToString();
        PlayerPrefsManager.SetPlayerName(newName);
        nameDisplay.GetComponent<DisplayName>().GetName();
        changeNameButton.SetActive(true);
        quitButton.SetActive(true);
        inputMenu.SetActive(false);
        authenticator.GetRegistrationData();
        authenticator.AuthRequest();
    }
}