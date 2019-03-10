using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;

public class Authenticator : MonoBehaviour
{
    string playerName;
    string password;
    bool isAuthenticated;
    string errorKey = "DETAILS";

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        Debug.Log("Authenticator Created");

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            print("Duplicate authenticator self-destructing!");

        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        GetRegistrationData();
        AuthRequest();
       // GS.Instance.GameSparksAuthenticated += AuthCheck;
    }

    public void AuthCheck(string obj)
    {
        isAuthenticated = GameSparks.Core.GS.Instance.Authenticated;
        Debug.Log("Processing autherntication for: " + playerName + " - " + isAuthenticated);
    }

    public void GetRegistrationData()
    {
        playerName = PlayerPrefsManager.GetPlayerName();
        if (playerName == "")
        {
            Debug.Log("Error while reading player name, proceeding with deafault value");
            playerName = "Player";
        }
        password = "dupadupa1234";
    }

    public void RegisterRequest()
    {
        Debug.Log("Registering user: " + playerName);
        new RegistrationRequest()
            .SetDisplayName(playerName)
            .SetPassword(password)
            .SetUserName(playerName)
            .Send((response) => {
                string authToken = response.AuthToken;
                string displayName = response.DisplayName;
                bool? newPlayer = response.NewPlayer;
                var switchSummary = response.SwitchSummary;
                string userId = response.UserId;
                AuthRequest();
            });
    }

    public void AuthRequest()
    {
        Debug.Log("Authenticating user: " + playerName);
        new AuthenticationRequest()
            .SetPassword(password)
            .SetUserName(playerName)
            .Send((response) => {
                string authToken = response.AuthToken;
                string displayName = response.DisplayName;
                bool? newPlayer = response.NewPlayer;
                var switchSummary = response.SwitchSummary;
                string userId = response.UserId;
                if (response.HasErrors)
                {
                    string error = response.Errors.BaseData[errorKey].ToString();
                    if (error == "UNRECOGNISED")
                    {
                        Debug.Log("Player unrecognized, proceed with registration.");
                        RegisterRequest();
                    }
                    else if (error == "LOCKED")
                    {
                        Debug.Log("Account is locked.");
                    }
                } else
                {
                    Debug.Log("Authentication Successful!");
                }
            });
    }
}
