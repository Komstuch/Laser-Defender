﻿using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 150;
	public GameObject projectile;
	public float projectileSpeed = 10f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	public AudioClip fireSound;
	public AudioClip deathSound;
    float fireSoundVolume;
    float deathSoundVolume;

    private ScoreKeeper scoreKeeper;
	
	void Start(){
        SetVFXVolume();
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.getDamage();
			missile.Hit();
			if(health <=0 ){
				Destroy (gameObject);
				scoreKeeper.AddScore(scoreValue);
				AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
			}		
		}
	}
	
	void Update (){
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability){
			Fire();
		}
	}
	
	void Fire(){
		Vector3 startPosition = transform.position + new Vector3(0f, -1f, 0f);
		GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position, fireSoundVolume);
	}
    private void SetVFXVolume()
    {
        if (PlayerPrefsManager.GetMasterVolume() == 0f)
        {
            fireSoundVolume = 0f;
            deathSoundVolume = 0f;
        }
        else
        {
            fireSoundVolume = 0.3f;
            deathSoundVolume = 0.25f;
        }
    }
}
