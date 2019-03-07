using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] float volume;
    Image currentImage;
    MusicPlayer musicPlayer;

    void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        currentImage = GetComponent<Image>();
        volume = musicPlayer.gameObject.GetComponent<AudioSource>().volume;

        if(volume > 0)
        {
            currentImage.sprite = sprites[0];
        }
        else
        {
            currentImage.sprite = sprites[1];
        }
        
    }

    public void MuteClicked()
    {
        if (currentImage.sprite == sprites[0]) // Clicket to mute
        {
            currentImage.sprite = sprites[1];
            musicPlayer.Mute();
            PlayerPrefsManager.SetMasterVolume(0f);
        }
        else if (currentImage.sprite == sprites[1]) // Clicked to unmute
        {
            currentImage.sprite = sprites[0];
            musicPlayer.UnMute(0.5f);
        }
    }
}
