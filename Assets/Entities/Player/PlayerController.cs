using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

	[SerializeField] float moveSpeed = 15.0f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 15f;

    private float xMin, xMax, yMin, yMax;

	public float firingRate = 0.2f;
	public float health = 300f;
	public AudioClip fireSound;
	
	void Start(){
        SetMoveboundaries();
	}
	
	void Update () {
	
		if(Input.GetButtonDown("Fire1")){
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		if(Input.GetButtonUp("Fire1")){
			CancelInvoke("Fire");
		}

        Move();		
	}

    private void Fire()
    {
        Vector3 offset = new Vector3(0f, 0.5f, 0f);

        GameObject laserBeam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
        laserBeam.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, projectileSpeed, 0f);
        AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position, 0.3f);
    }

    void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			Debug.Log ("Player Collided with the missile");
			health -= missile.getDamage();
			missile.Hit();
			if(health <=0 ){
				Die();
			}		
		}
	}
	
	void Die(){
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");
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
