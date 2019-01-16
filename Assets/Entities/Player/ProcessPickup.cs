using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessPickup : MonoBehaviour
{
    string pickupProperty;
    float pickupValue;

    PlayerController playerController;
    HealthDisplay healthDisplay;
    ScoreKeeper scoreKeeper;
    AnalyticsManager analyticsManager;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        healthDisplay = FindObjectOfType<HealthDisplay>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        analyticsManager = FindObjectOfType<AnalyticsManager>();
    }

    public void Process(Pickup pickup)
    {
        pickupProperty = pickup.GetPickupProperty();
        pickupValue = pickup.GetPickupValue();
        analyticsManager.AddPickupsTaken();

        switch (pickupProperty)
        {
            case "Health":
                playerController.AddHealth((int)pickupValue);
                healthDisplay.SetHealth(playerController.GetHealth());
                analyticsManager.AddHealthPickups();
                break;

            case "Points":
                scoreKeeper.AddScore((int)pickupValue);
                analyticsManager.AddPointPickups();
                break;

            case "Damage":
                playerController.SetProjectileDamage(pickupValue);
                analyticsManager.AddDamagePickups();
                break;

            case "ShotSpeed":
                playerController.IncreaseFiringRate(pickupValue);
                analyticsManager.AddSpeedPickups();
                break;

            default:
                Debug.Log("Incorrect Pickup Property!");
                break;
        }
        pickup.Pick();
    }
}
