using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] string pickupProperty;
    [SerializeField] float pickupValue;

    public string GetPickupProperty() { return pickupProperty; }
    public float GetPickupValue() { return pickupValue; }
}
