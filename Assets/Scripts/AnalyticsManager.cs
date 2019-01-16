using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    int enemiesKilled;
    int pickupsTaken;
    long scoreGained;
    int healthPickups;
    int damagePickups;
    int speedPickups;
    int pointPickups;

    private void Start()
    {
        scoreGained = 0;
        pickupsTaken = 0;
        enemiesKilled = 0;
        healthPickups = 0;
        damagePickups = 0;
        speedPickups = 0;
        pointPickups = 0;
    }

    public void AddEnemies() { enemiesKilled += 1; }
    public void AddPickupsTaken() { pickupsTaken += 1; }
    public void AddHealthPickups() { healthPickups += 1; }
    public void AddSpeedPickups() { speedPickups += 1; }
    public void AddDamagePickups() { damagePickups += 1; }
    public void AddPointPickups() { pointPickups += 1; }

    public void PostResults()
    {
        scoreGained = ScoreKeeper.score;

        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
        {
            {"Score", scoreGained},
            {"Total # of Pickups", pickupsTaken },
            {"Enemies Killed", enemiesKilled },
            {"# of damage pickups", damagePickups },
            {"# of health pickups", healthPickups },
            {"# of speed pickups", speedPickups },
            {"# of point pickups", pointPickups },
        });
    }

    //TODO - Set event for wave cycle completion
}
