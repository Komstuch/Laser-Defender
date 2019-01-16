using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;

public class ScorePoster : MonoBehaviour
{
    int finalScore;
    private void Start()
    {
        finalScore = ScoreKeeper.score;
        Debug.Log("Final Score: " + finalScore.ToString());
        PostScore(finalScore);
    }

    private void PostScore(int score)
    {
        new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey("SUBMIT_SCORE")
            .SetEventAttribute("SCORE", score.ToString())
            .Send((response) => ProcessPosting(response));
    }

    private void ProcessPosting(LogEventResponse response)
    {
        if (!response.HasErrors)
        {
            Debug.Log("Score Posted Successfully..." + finalScore);
        }
        else
        {
            Debug.Log("Error Posting Score...");
        }
    }
}
