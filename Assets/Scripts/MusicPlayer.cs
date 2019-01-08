using UnityEngine;
using System.Collections;
using System;

public class MusicPlayer : MonoBehaviour {
	
	[SerializeField] AudioClip startClip;
    [SerializeField] AudioClip gameClip;
    [SerializeField] AudioClip endClip;
	
	private AudioSource music;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        Debug.Log("Music player Created");

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            print("Duplicate music player self-destructing!");

        } else
        {
            DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }
    }
	
	void OnLevelWasLoaded(int level){
		Debug.Log ("Music player: loaded level " + level);
		music = GetComponent<AudioSource>();
		music.Stop();
		
		if(level==0){
			music.clip = startClip;
		}
		if(level==1){
			music.clip = gameClip;
		}
		if(level==2){
			music.clip = endClip;
		}
			music.loop = true;
			music.Play();
	}
}
