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

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        healthDisplay = FindObjectOfType<HealthDisplay>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void Process(Pickup pickup)
    {
        pickupProperty = pickup.GetPickupProperty();
        pickupValue = pickup.GetPickupValue();

        switch (pickupProperty)
        {
            case "Health":
                playerController.AddHealth((int)pickupValue);
                healthDisplay.SetHealth(playerController.GetHealth());
                break;

            case "Points":
                scoreKeeper.AddScore((int)pickupValue);
                break;

            case "Damage":
                playerController.SetProjectileDamage(pickupValue);
                break;

            case "ShotSpeed":
                playerController.IncreaseFiringRate(pickupValue);
                break;

            default:
                Debug.Log("Incorrect Pickup Property!");
                break;
        }
        pickup.Pick();
    }
}
