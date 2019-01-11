using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] string pickupProperty;
    [SerializeField] float pickupValue;
    [SerializeField] AudioClip pickupSound;
    [SerializeField] GameObject pickupEffect;

    float durationOfEffect;
    float pickupVolume = 0.2f;

    public string GetPickupProperty() { return pickupProperty; }
    public float GetPickupValue() { return pickupValue; }

    private void Start()
    {
        durationOfEffect = pickupEffect.GetComponent<ParticleSystem>().main.duration;
    }
    public void Pick()
    {
        AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position, pickupVolume);
        GameObject effect = Instantiate(pickupEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(effect, durationOfEffect);
    }
}
