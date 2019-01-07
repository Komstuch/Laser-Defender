using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    [Header("Player")]
	[SerializeField] float moveSpeed = 15.0f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 15f;
    [SerializeField] float firingRate = 0.2f;

    [Header("Audio Effects")]
    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float fireSoundVolume = 0.3f;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.15f;

    Coroutine firingCoroutine;

    private float xMin, xMax, yMin, yMax;
	
	void Start(){
        SetMoveboundaries();
	}
	
	void Update ()
    {
        HandleFire();
        Move();
    }

    private void HandleFire() {
        if (Input.GetButtonDown("Fire1")) {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Fire1")){
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinously()
    {
        while (true){
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
        if (!projectile) { return; }
        ProcessHit(projectile);
    }

    private void ProcessHit(DamageDealer projectile)
    {
            Debug.Log("Player Collided with the missile");
            health -= projectile.GetDamage();
            projectile.Hit();
            if (health <= 0)
            {
                Die();
            }
    }

    private void Die(){
        //LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        //man.LoadLevel("Win Screen");
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        Destroy (gameObject);		
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
}
