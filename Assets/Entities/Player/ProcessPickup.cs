using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessPickup : MonoBehaviour
{
    string pickupProperty;
    float pickupValue;

    PlayerController playerController;
    HealthDisplay healthDisplay;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        healthDisplay = FindObjectOfType<HealthDisplay>();
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

            default:
                Debug.Log("Incorrect Pickup Property!");
                break;
        }
        pickup.Pick();
    }
}
