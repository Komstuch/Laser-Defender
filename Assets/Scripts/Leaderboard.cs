using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    ILeaderboard m_Leaderboard;
    [SerializeField] Text text1;
    [SerializeField] Text text2;

    void Start()
    {
        Social.localUser.Authenticate(success => DidAuthenticate(success));

        DoLeaderboard();
        ReportScore(1000, m_Leaderboard.id);
        text1.text = Social.localUser.userName;
    }

    void DoLeaderboard()
    {
        m_Leaderboard = Social.CreateLeaderboard();
        m_Leaderboard.id = "LeaderboardTEST";
        m_Leaderboard.LoadScores(result => DidLoadLeaderboard(result));
    }

    void DidLoadLeaderboard(bool result)
    {
        Debug.Log("Received " + m_Leaderboard.scores.Length + " scores");
        foreach (IScore score in m_Leaderboard.scores)
            Debug.Log(score);
    }

    void DidAuthenticate(bool success)
    {
        if (success)
        {
            Debug.Log("Authentication successful");
            string userInfo = "Username: " + Social.localUser.userName +
                "\nUser ID: " + Social.localUser.id;
            Debug.Log(userInfo);
        }
        else
            Debug.Log("Authentication failed");
    }

    void ReportScore(long score, string leaderboardID)
    {
        Social.ReportScore(score, leaderboardID, success =>
        {
            if (success)
            {
                Debug.Log("Reported score successfully");
                text2.text = "Success";
            }
            else
            {
                Debug.Log("Failed to report score");
                text2.text = "Fail";
            }

        });
    }

}
