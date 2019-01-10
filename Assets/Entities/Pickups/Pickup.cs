using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] string pickupProperty;
    [SerializeField] float pickupValue;
    [SerializeField] AudioClip pickupSound;
    float pickupVolume = 0.2f;

    public string GetPickupProperty() { return pickupProperty; }
    public float GetPickupValue() { return pickupValue; }

    public void Pick()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position, pickupVolume);
    }
}
