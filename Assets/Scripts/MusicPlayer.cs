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
            SetInitialMusicVolume();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }
    }

    private void SetInitialMusicVolume()
    {
        if (PlayerPrefsManager.CheckMasterVolumeKey()){
            music.volume = PlayerPrefsManager.GetMasterVolume();
        }
        else
        {
            music.volume = 0.5f;
        }

    }

    void OnLevelWasLoaded(int level) {
        Debug.Log("Music player: loaded level " + level);
        music = GetComponent<AudioSource>();

        if (level == 0) {
            music.Stop();
            music.clip = startClip;
            music.Play();
        }
        if (level == 2) {
            music.Stop();
            music.clip = gameClip;
            music.Play();
        }
        if (level == 3) {
            music.Stop();
            music.clip = endClip;
            music.Play();
        }
    }

    public void Mute()
    {
        music.volume = 0f;
        PlayerPrefsManager.SetMasterVolume(0f);
    }

    public void UnMute(float vol)
    {
        music.volume = vol;
        PlayerPrefsManager.SetMasterVolume(0.5f);
    }
}
