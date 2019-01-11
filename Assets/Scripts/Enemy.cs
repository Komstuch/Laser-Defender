using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Enemy")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Shooting")]
    [SerializeField] GameObject projectile;
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Pick-Up's")]
    [SerializeField] GameObject pickupList;
    [SerializeField] [Range(0, 1)] float spawnPropability;

    [Header("VFX")]
    [SerializeField] GameObject explosionParticles;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("Audio Effects")]
    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,2)] float fireSoundVolume = 2f;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.1f;

    private void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);    
    }
    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject enemyProjectile = Instantiate(projectile, transform.position, Quaternion.Euler(0f, 0f, 180f));
        enemyProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position, fireSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
            SpawnPickup();
        }
    }

    public void Die()
    {
        FindObjectOfType<ScoreKeeper>().AddScore(scoreValue);

        GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        Destroy(gameObject);
        Destroy(explosion, durationOfExplosion);
    }

    private void SpawnPickup()
    {
        bool isSpawned = (UnityEngine.Random.Range(0f, 1f) < spawnPropability);
        Debug.Log("Spawn pickup?: " + isSpawned);
        if (isSpawned)
        {
            GameObject pickup = Instantiate(pickupList, transform.position, Quaternion.identity);
            pickup.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.1f);
        }
    }
}
