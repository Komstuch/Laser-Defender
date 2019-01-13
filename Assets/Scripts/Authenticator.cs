using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;

public class Authenticator : MonoBehaviour
{
    string playerName;
    string password;

    void Start()
    {
        //Verify if curren user is already authenticated
        ProcessRegistration();
    }

    public void ProcessRegistration()
    {
        playerName = PlayerPrefs.GetString("PlayerName");
        Debug.Log("Current user: " + playerName);
        if (playerName == "")
        {
            Debug.Log("Error while reading player name, proceeding with deafault value");
            playerName = "Player";
        }
        password = "dupadupa1234";

        RegisterRequest();
        AuthRequest();
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
                Debug.Log("New player?: " + newPlayer);
                var switchSummary = response.SwitchSummary;
                string userId = response.UserId;
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
                Debug.Log("Test: " + newPlayer);
                //if (newPlayer == true)
                //{
                //    Debug.Log("User not registered...");
                //    RegisterRequest();
                //}
                //else Debug.Log("Authentication successful!");

                var switchSummary = response.SwitchSummary;
                string userId = response.UserId;
            });
    }
}
