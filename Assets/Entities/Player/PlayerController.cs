using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

	[SerializeField] float moveSpeed = 15.0f;
    private float xMin, xMax;
    public float padding = 0.5f;
	public GameObject projectile;
	public float projectileSpeed = 5f;
	public float firingRate = 0.2f;
	public float health = 300f;
	public AudioClip fireSound;
	
	float xmin, xmax;
	
	void Start(){
		//float distance = transform.position.z - Camera.main.transform.position.z; // Distance between object and the camera in z direction
		//Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance)); //Lower left corner of our playspace in world coordinates
		//Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		//xmin = leftMost.x + padding;
		//xmax = rightMost.x - padding;
        SetMoveboundaries();
    
	}

    private void SetMoveboundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;

    }

    void Fire(){
		Vector3 offset = new Vector3(0f, 0.5f, 0f);
		GameObject laserBeam = Instantiate (projectile, transform.position+offset, Quaternion.identity)as GameObject;
		laserBeam.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, projectileSpeed, 0f);
		AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position, 0.3f);
	}
	
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}

        Move();		
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
        var newXPos = transform.position.x + deltaX * Time.deltaTime * moveSpeed;
        var newYPos = transform.position.y + deltaY * Time.deltaTime * moveSpeed;
        transform.position = new Vector3(newXPos, newYPos, transform.position.z);

        // Restrict the player to the gamespace
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
    }

}
