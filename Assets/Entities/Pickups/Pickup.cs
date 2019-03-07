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
    float pickupVolume;

    public string GetPickupProperty() { return pickupProperty; }
    public float GetPickupValue() { return pickupValue; }

    private void Start()
    {
        SetVFXVolume();
        durationOfEffect = pickupEffect.GetComponent<ParticleSystem>().main.duration;
    }
    public void Pick()
    {
        AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position, pickupVolume);
        GameObject effect = Instantiate(pickupEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(effect, durationOfEffect);
    }

    private void SetVFXVolume()
    {
        if (PlayerPrefsManager.GetMasterVolume() == 0f)
        {
            pickupVolume = 0f;
        }
        else
        {
            pickupVolume = 0.2f;
        }
    }
}
