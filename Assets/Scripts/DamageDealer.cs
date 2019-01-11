using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {

    [SerializeField] int damage = 100;
    
    public int GetDamage() { return damage; }

    public void Hit()
    {
        Destroy(gameObject);
    }

    public void IncreaseDamage(float multiplier)
    {
        float newDamage = damage * multiplier;
        damage = damage + (int)newDamage;
    }
}
