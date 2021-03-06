﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    SimpleAds simpleAds;

    public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
        if (name == "Start Menu") ScoreKeeper.Reset();

        if(FindObjectOfType<SimpleAds>() & SceneManager.GetActiveScene().name == "Win Screen") { FindObjectOfType<SimpleAds>().DestroyBanner(); }

        SceneManager.LoadScene(name);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("Win Screen", 2));

    }
    public IEnumerator WaitAndLoad(string name, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(name);
    }

    private void LoadStartScene()
    {
        StartCoroutine(WaitAndLoad("Start Menu", 3));
    }
}
