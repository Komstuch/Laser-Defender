using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource music;
	
	void Start () {
	Debug.Log ("Music player Created");
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
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
		Debug.Log("Play start");
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
