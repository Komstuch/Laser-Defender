using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    int entryCount = 10;
    int includeFirst = 10;
    string leaderboardShortCode = "SCORE_LEADERBOARD";
    string scoreKey = "SCORE";

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
        .Send((response) => ProcessData(response));
    }

    private void ProcessData(LeaderboardDataResponse response)
    {
        string challengeInstanceId = response.ChallengeInstanceId;
        GSEnumerable<LeaderboardDataResponse._LeaderboardData> data = response.Data;
        string leaderboardShortCode = response.LeaderboardShortCode;
        GSData scriptData = response.ScriptData;
        foreach (LeaderboardDataResponse._LeaderboardData entry in data)
        {
            string score = entry.JSONData[scoreKey].ToString();
            string name = entry.UserName.ToString();
            Debug.Log("Player: " + name + ", Score: " + score);
        }
    }
}

