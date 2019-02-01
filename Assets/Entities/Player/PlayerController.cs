using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    [Header("Player")]
    public float moveSpeed = 15.0f;
    public float padding = 0.5f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 15f;
    [SerializeField] float firingRate = 0.2f;

    [Header("Audio Effects")]
    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] [Range(0, 1)] float fireSoundVolume = 0.3f;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.15f;
    [SerializeField] [Range(0, 1)] float hitSoundVolume = 0.1f;

    [Header("Particla Effects")]
    [SerializeField] GameObject deathEffect;
    float deathEffectDuration;

    AnalyticsManager analyticsManager;
    Coroutine firingCoroutine;
    HealthDisplay healthDisplay;
    ProcessPickup pickupProcessor;
    EngineThruster engineThruster;

    private float xMin, xMax, yMin, yMax;
    private bool firingOn = false;
    private string pickupProperty;
    private float pickupValue;

    void Start() {
        SetMoveboundaries();
        healthDisplay = FindObjectOfType<HealthDisplay>();
        deathEffectDuration = deathEffect.GetComponent<ParticleSystem>().main.duration;
        pickupProcessor = GetComponent<ProcessPickup>();
        analyticsManager = FindObjectOfType<AnalyticsManager>();
        engineThruster = FindObjectOfType<EngineThruster>();
    }

    void Update()
    {
        HandleFire();
        Move();
    }

    public void HandleFireButton()
    {
        firingOn = true;
        firingCoroutine = StartCoroutine(FireContinously());
    }

    public void StopFireButton()
    {
        firingOn = false;
        StopCoroutine(firingCoroutine);
    }

    private void HandleFire() {
        if (Input.GetButtonDown("Fire1") & firingOn == false) {
            firingOn = true;
            firingCoroutine = StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Fire1") & firingOn == true) {
            firingOn = false;
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinously()
    {
        while (true) {
            Fire();
            yield return new WaitForSeconds(firingRate);
        }
    }

    private void Fire()
    {
        Vector3 offset = new Vector3(0f, 0.5f, 0f);

        GameObject laserBeam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
        laserBeam.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, projectileSpeed, 0f);
        AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position, fireSoundVolume);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        DamageDealer projectile = collider.gameObject.GetComponent<DamageDealer>();
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        Pickup pickup = collider.gameObject.GetComponent<Pickup>();
        if (projectile)
        {
            ProcessHit(projectile, enemy);
        } else if (pickup)
        {
            pickupProcessor.Process(pickup);
        } else {
            return;
        }
    }

    private void ProcessHit(DamageDealer damageDealer, Enemy enemy)
    {
        health -= damageDealer.GetDamage();
        healthDisplay.SetHealth(health);
        if (enemy) {
            enemy.Die();
        }
        else {
            damageDealer.Hit();
        }

        if (health <= 0) {
            Die();
        } else {
            AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, hitSoundVolume);
        }
    }

    private void Die() {
        analyticsManager.PostResults();
        LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        man.LoadGameOver();
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(effect, deathEffectDuration);
        engineThruster.KillEngine();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal");
        var deltaY = Input.GetAxis("Vertical");
        var newXPos = transform.position.x + deltaX * Time.deltaTime * moveSpeed; //Frame-rate independent
        var newYPos = transform.position.y + deltaY * Time.deltaTime * moveSpeed; //Frame-rate independent

        // Restrict the player to the gamespace
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);
        newYPos = Mathf.Clamp(newYPos, yMin, yMax);

        transform.position = new Vector3(newXPos, newYPos, transform.position.z);
        engineThruster.transform.position = transform.position + new Vector3 (0f, -0.4f, 0f);
    }

    private void SetMoveboundaries()
    {
        Camera gameCamera = Camera.main;
        float distance = transform.position.z - Camera.main.transform.position.z; // Distance between object and the camera in z direction

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, distance)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, distance)).y - padding;
    }

    public int GetHealth() { return health; }

    public void AddHealth(int health)
    {
        this.health += health;
    }
    
    public void SetProjectileDamage(float multiplier)
    {
        projectile.GetComponent<DamageDealer>().IncreaseDamage(multiplier);
    }

    public void IncreaseFiringRate(float multiplier)
    {
        firingRate = firingRate - firingRate * multiplier;
    }
}
