using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    int entryCount = 10;
    int includeFirst = 10;
    string leaderboardShortCode = "SCORE_LEADERBOARD";
    string scoreKey = "SCORE";
    [SerializeField] List<string> playerScores;
    [SerializeField] List<string> playerNames;

    [SerializeField] List<GameObject> playerScoresText;
    [SerializeField] List<GameObject> playerNamesText;

    private void Start()
    {
        GetLeaderboardFromGS();
    }

    private void GetLeaderboardFromGS()
    {
        Debug.Log("Getting leaderboard from GameSpark...");
        new LeaderboardDataRequest()
        .SetEntryCount(entryCount)
        .SetIncludeFirst(includeFirst)
        .SetLeaderboardShortCode(leaderboardShortCode)
        .Send((response) => ProcessLeaderBoardData(response));
    }

    private void ProcessLeaderBoardData(LeaderboardDataResponse response)
    {
        string challengeInstanceId = response.ChallengeInstanceId;
        GSEnumerable<LeaderboardDataResponse._LeaderboardData> data = response.Data;
        string leaderboardShortCode = response.LeaderboardShortCode;
        GSData scriptData = response.ScriptData;
        foreach (LeaderboardDataResponse._LeaderboardData entry in data)
        {
            string score = entry.JSONData[scoreKey].ToString();
            Debug.Log("Score raw: " + score);
            string name = entry.UserName.ToString();
            playerScores.Add(score);
            playerNames.Add(name);
        }
        DisplayLeaderboard();
    }

    private void DisplayLeaderboard()
    {
        for(int i =0; i< entryCount; i++){
            Debug.Log("Score: " + playerScores[i]);
            playerScoresText[i].GetComponent<Text>().text = playerScores[i];
            playerNamesText[i].GetComponent<Text>().text = playerNames[i];
        }
    }
}

